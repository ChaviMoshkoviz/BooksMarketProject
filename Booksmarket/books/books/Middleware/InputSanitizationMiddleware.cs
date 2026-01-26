using Ganss.Xss;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace books.Middleware
{
    public class InputSanitizationMiddleware
    {
        private readonly RequestDelegate _next;

        public InputSanitizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // מטפל רק בבקשות מסוג POST ו-PUT המכילות גוף (Body)
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();

                    if (!string.IsNullOrEmpty(body))
                    {
                        var sanitizer = new HtmlSanitizer();

                        // --- הגדרה לניקוי מוחלט: מסירים את כל התגיות המותרות ---
                        // זה יגרום לכך ששום תגית HTML (כמו <b>, <img>, <a>) לא תעבור.
                        sanitizer.AllowedTags.Clear();
                        sanitizer.AllowedAttributes.Clear();
                        sanitizer.AllowedCssProperties.Clear();
                        // ------------------------------------------------------

                        try
                        {
                            var jsonNode = JsonNode.Parse(body);
                            SanitizeNode(jsonNode, sanitizer);

                            // הגדרות קידוד שתומכות בעברית בצורה מלאה
                            var options = new JsonSerializerOptions
                            {
                                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                                WriteIndented = false
                            };

                            // המרה למחרוזת JSON תקינה
                            string sanitizedBody = JsonSerializer.Serialize(jsonNode, options);

                            // יצירת ה-Bytes עם קידוד UTF8
                            var bytes = Encoding.UTF8.GetBytes(sanitizedBody);

                            // עדכון ה-Body וה-ContentLength (קריטי למניעת שגיאות 400)
                            context.Request.Body = new MemoryStream(bytes);
                            context.Request.ContentLength = bytes.Length;
                        }
                        catch (JsonException)
                        {
                            // במקרה של JSON לא תקין, לא נבצע שינויים
                        }
                    }
                }
                context.Request.Body.Position = 0;
            }

            await _next(context);
        }

        private void SanitizeNode(JsonNode node, HtmlSanitizer sanitizer)
        {
            if (node is JsonObject obj)
            {
                foreach (var property in obj.ToList())
                {
                    if (property.Value is JsonValue val && val.TryGetValue<string>(out var str))
                    {
                        // ניקוי המחרוזת מכל תגית HTML
                        obj[property.Key] = JsonValue.Create(sanitizer.Sanitize(str));
                    }
                    else if (property.Value != null)
                    {
                        SanitizeNode(property.Value, sanitizer);
                    }
                }
            }
            else if (node is JsonArray array)
            {
                foreach (var item in array)
                {
                    if (item != null) SanitizeNode(item, sanitizer);
                }
            }
        }
    }
}