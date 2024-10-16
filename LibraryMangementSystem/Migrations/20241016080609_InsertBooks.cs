using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMangementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InsertBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
           table: "Books",
           columns: new[] { "Title", "Image", "Author", "Genre", "ISBN", "PublishedYear", "AvailableCopies" },
           values: new object[,]
               {
                { "The Great Gatsby", "great.jpg", "F. Scott Fitzgerald", "Classic Fiction", "9780743273565", 1925, 15 },
        { "1984", "1984.jpg", "George Orwell", "Dystopian Fiction", "9780451524935", 1949, 25 },
        { "To Kill a Mockingbird", "tokill.jpg", "Harper Lee", "Classic Fiction", "9780060935467", 1960, 10 },
        { "Pride and Prejudice", "pride.jpg", "Jane Austen", "Romance", "9780141040349", 1813, 8 },
        { "Moby-Dick", "mobydick.jpg", "Herman Melville", "Adventure", "9780142437247", 1851, 7 },
        { "War and Peace", "warpeace.jpg", "Leo Tolstoy", "Historical Fiction", "9780199232765", 1869, 12 },
        { "The Catcher in the Rye", "catcher.jpg", "J.D. Salinger", "Fiction", "9780316769488", 1951, 30 },
        { "Brave New World", "brave.jpg", "Aldous Huxley", "Science Fiction", "9780060850524", 1932, 16 },
        { "The Odyssey", "odyssey.jpg", "Homer", "Epic", "9780140268867", 1614, 10 },
        { "Ulysses", "ulysses.jpg", "James Joyce", "Modernist Fiction", "9781840226355", 1922, 5 },
        { "Crime and Punishment", "crime.jpg", "Fyodor Dostoevsky", "Psychological Fiction", "9780140449136", 1866, 9 },
        { "The Brothers Karamazov", "brothers.jpg", "Fyodor Dostoevsky", "Philosophical Fiction", "9780374528379", 1880, 14 },
        { "Madame Bovary", "bovary.jpg", "Gustave Flaubert", "Fiction", "9780140449129", 1856, 6 },
        { "The Hobbit", "hobbit.jpg", "J.R.R. Tolkien", "Fantasy", "9780261102217", 1937, 22 },
        { "Lord of the Flies", "flies.jpg", "William Golding", "Allegorical Fiction", "9780571200535", 1954, 18 },
        { "The Divine Comedy", "divine.jpg", "Dante Alighieri", "Epic Poetry", "9780140448955", 1320, 4 },
        { "The Stranger", "stranger.jpg", "Albert Camus", "Philosophical Fiction", "9780679720201", 1942, 12 },
        { "One Hundred Years of Solitude", "solitude.jpg", "Gabriel Garcia Marquez", "Magical Realism", "9780060883287", 1967, 9 },
        { "Don Quixote", "quixote.jpg", "Miguel de Cervantes", "Adventure", "9780060934347", 1605, 20 },
        { "The Picture of Dorian Gray", "dorian.jpg", "Oscar Wilde", "Gothic Fiction", "9780141439570", 1890, 13 },
        { "Anna Karenina", "anna.jpg", "Leo Tolstoy", "Romance", "9780143035008", 1877, 7 },
        { "The Sound and the Fury", "fury.jpg", "William Faulkner", "Southern Gothic", "9780679732242", 1929, 6 },
        { "Les Misérables", "lesmis.jpg", "Victor Hugo", "Historical Fiction", "9780451419439", 1862, 11 },
        { "The Grapes of Wrath", "grapes.jpg", "John Steinbeck", "Social Commentary", "9780143039433", 1939, 19 },
        { "Frankenstein", "frankenstein.jpg", "Mary Shelley", "Horror", "9780143131847", 1818, 14 },
        { "Wuthering Heights", "wuthering.jpg", "Emily Brontë", "Gothic Fiction", "9780141439556", 1847, 23 },
        { "The Old Man and the Sea", "oldman.jpg", "Ernest Hemingway", "Fiction", "9780684801223", 1952, 17 },
        { "Heart of Darkness", "heart.jpg", "Joseph Conrad", "Novella", "9780141441672", 1899, 8 },
        { "Dracula", "dracula.jpg", "Bram Stoker", "Gothic Fiction", "9780141439846", 1897, 10 },
        { "A Tale of Two Cities", "tale.jpg", "Charles Dickens", "Historical Fiction", "9780141439600", 1859, 4 },
        { "The Count of Monte Cristo", "count.jpg", "Alexandre Dumas", "Adventure", "9780140449266", 1844, 25 },
        { "Alice's Adventures in Wonderland", "alice.jpg", "Lewis Carroll", "Fantasy", "9780141439761", 1865, 27 },
        { "Fahrenheit 451", "fahrenheit.jpg", "Ray Bradbury", "Dystopian Fiction", "9781451673319", 1953, 16 },
        { "Beloved", "beloved.jpg", "Toni Morrison", "Historical Fiction", "9781400033416", 1987, 11 },
        { "Jane Eyre", "jane.jpg", "Charlotte Brontë", "Romance", "9780141441146", 1847, 9 },
        { "The Road", "road.jpg", "Cormac McCarthy", "Post-apocalyptic Fiction", "9780307387899", 2006, 26 },
        { "The Kite Runner", "kite.jpg", "Khaled Hosseini", "Drama", "9781594631931", 2003, 21 },
        { "A Clockwork Orange", "clockwork.jpg", "Anthony Burgess", "Science Fiction", "9780393312836", 1962, 20 },
        { "Slaughterhouse-Five", "slaughterhouse.jpg", "Kurt Vonnegut", "Science Fiction", "9780440180296", 1969, 24 },
        { "The Alchemist", "alchemist.jpg", "Paulo Coelho", "Fiction", "9780061122415", 1988, 18 },
        { "The Little Prince", "prince.jpg", "Antoine de Saint-Exupéry", "Fable", "9780156012195", 1943, 12 },
        { "Invisible Man", "invisible.jpg", "Ralph Ellison", "Fiction", "9780679732761", 1952, 9 },
        { "The Shining", "shining.jpg", "Stephen King", "Horror", "9780307743657", 1977, 17 },
        { "The Handmaid's Tale", "handmaid.jpg", "Margaret Atwood", "Dystopian Fiction", "9780385490818", 1985, 11 },
        { "The Giver", "giver.jpg", "Lois Lowry", "Young Adult Fiction", "9780544336261", 1993, 22 },
        { "Life of Pi", "pi.jpg", "Yann Martel", "Adventure", "9780156027328", 2001, 28 },
        { "The Secret Garden", "secret.jpg", "Frances Hodgson Burnett", "Children's Fiction", "9780141336503", 1911, 12 },
        { "The Hunger Games", "hunger.jpg", "Suzanne Collins", "Dystopian Fiction", "9780439023528", 2008, 30 },
        { "Harry Potter and the Philosopher's Stone", "hp1.jpg", "J.K. Rowling", "Fantasy", "9780747532699", 1997, 29 }
    }
           );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
