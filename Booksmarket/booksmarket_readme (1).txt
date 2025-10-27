# BooksMarket / BooksMarket

**מחבר / Author:** חוי מושקוביץ / Chavi Mushkowitz  
**סוג הפרויקט / Project Type:** Web API  
**שפת פיתוח / Language:** C#  
**סביבת פיתוח / Development Environment:** Visual Studio  
**סטטוס / Status:** API פעיל, צד לקוח בעתיד / Active API, client-side in the future  

---

## תיאור הפרויקט / Project Description
BooksMarket היא פלטפורמה למסירה או מכירה של ספרים משומשים בין משתמשים. האתר מיועד לכל אוהבי הספרים, ומאפשר למשתמשים לנהל ספרים, מודעות וקשרים בין משתמשים.

BooksMarket is a platform for exchanging or selling used books between users. The site is intended for all book lovers and allows users to manage books, listings, and connections between users.

---

## טכנולוגיות ושפות / Technologies & Languages
- **C# / .NET Web API**  
- **Entity Framework** (למאגר נתונים עתידי / for future database)  
- **LINQ**  
- **JSON Serialization**  
- **Swagger** (לתיעוד ה־API / for API documentation)  

---

## מבנה המחלקות / Classes Structure

### 1. Book / ספר
- **פעולות / Actions:**
  - `[HttpGet]` - הצגת כל הספרים / Get all books  
  - `[HttpGet("author/{author}")]` - הצגה לפי מחבר הספר / Get by author  
  - `[HttpGet("genre/{genre}")]` - סינון לפי סגנון / Filter by genre  
  - `[HttpPost]` - הוספת ספר / Add a new book  

### 2. User / משתמש
- **פעולות / Actions:**
  - `[HttpGet]` - הצגת כל המשתמשים / Get all users  
  - `[HttpGet("{UserId}")]` - הצגה לפי מזהה משתמש / Get by user ID  
  - `[HttpPost("register")]` - הוספת משתמש חדש / Register new user  
  - `[HttpPut("{id}")]` - עדכון פרטי משתמש / Update user  
  - `[HttpPut("deactivate/{id}")]` - עדכון סטטוס ללא פעיל / Deactivate user  

### 3. Listings / מודעות
- **פעולות / Actions:**
  - `[HttpGet]` - הצגת כל המודעות הפעילות / Get all active listings  
  - `[HttpGet("byUser/{userId}")]` - הצגת מודעות לפי משתמש / Get listings by user  
  - `[HttpGet("byOrice")]` - סינון לפי טווח מחירים / Filter by price range  
  - `[HttpPost]` - יצירת מודעה חדשה / Create new listing  
  - `[HttpPut("{id}")]` - עדכון מודעה / Update listing  
  - `[HttpDelete("{id}")]` - "מחיקה רכה" - הפיכת מודעה ללא פעילה / Soft delete listing  

---

## מבנה ספריות וקבצים / File Structure
(כאן אפשר להוסיף בעתיד את מבנה הקבצים / Future: add file structure here)

---

## התקנה והרצה / Installation & Run
1. הורד/שכפל את הפרויקט / Clone the project:  
   ```bash
   git clone <url-of-repo>
   ```
2. פתח את הפרויקט ב־Visual Studio / Open the project in Visual Studio  
3. הרץ את ה־API באמצעות IIS Express או הפקודה `F5` / Run the API using IIS Express or press `F5`  
4. בדוק את נקודות הקצה באמצעות Swagger או Postman / Test endpoints via Swagger or Postman  

---

## סיכום / מטרות לעתיד / Summary & Future Goals
- חיבור למסד נתונים אמיתי במקום מערכים / Connect to a real database instead of arrays  
- פיתוח צד לקוח (Frontend) / Develop a client-side frontend  
- הוספת מערכת חיפושים וסינון מתקדמת / Add advanced search and filtering  
- הוספת מערכת הודעות בין משתמשים / Add messaging system between users  

---

## תרומות / Contributions
כל תרומה היא ברוכה ומתקבלת בברכה. אם תרצה להציע שיפורים, אנא פתח Issue או Pull Request. / Contributions are welcome. Please open an Issue or Pull Request to suggest improvements.

---

## רישיון / License
[MIT License] או לפי הרישיון שתבחר / or choose your preferred license
