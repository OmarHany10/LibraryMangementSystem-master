using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMangementSystem.Migrations
{
    /// <inheritdoc />
    public partial class insertbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Books",
            columns: new[] { "Title", "Image", "Author", "Genre", "ISBN", "PublishedYear", "AvailableCopies" },
            values: new object[,]
                {
                { "The Great Gatsby", "great.jpg", "F. Scott Fitzgerald", "Classic Fiction", "9780743273565", 1925, 3 },
                { "1984", "1984.jpg", "George Orwell", "Dystopian Fiction", "9780451524935", 1949, 5 },
                { "To Kill a Mockingbird", "tokill.jpg", "Harper Lee", "Classic Fiction", "9780060935467", 1960, 4 },
                { "Pride and Prejudice", "pride.jpg", "Jane Austen", "Romance", "9780141040349", 1813, 2 },
                { "Moby-Dick", "mobydick.jpg", "Herman Melville", "Adventure", "9780142437247", 1851, 1 },
                { "War and Peace", "warpeace.jpg", "Leo Tolstoy", "Historical Fiction", "9780199232765", 1869, 2 },
                { "The Catcher in the Rye", "catcher.jpg", "J.D. Salinger", "Fiction", "9780316769488", 1951, 6 },
                { "Brave New World", "brave.jpg", "Aldous Huxley", "Science Fiction", "9780060850524", 1932, 5 },
                { "The Odyssey", "odyssey.jpg", "Homer", "Epic", "9780140268867", 1614, 3 },
                { "Ulysses", "ulysses.jpg", "James Joyce", "Modernist Fiction", "9781840226355", 1922, 1 },
                { "Crime and Punishment", "crime.jpg", "Fyodor Dostoevsky", "Psychological Fiction", "9780140449136", 1866, 2 },
                { "The Brothers Karamazov", "brothers.jpg", "Fyodor Dostoevsky", "Philosophical Fiction", "9780374528379", 1880, 3 },
                { "Madame Bovary", "bovary.jpg", "Gustave Flaubert", "Fiction", "9780140449129", 1856, 2 },
                { "The Hobbit", "hobbit.jpg", "J.R.R. Tolkien", "Fantasy", "9780261102217", 1937, 5 },
                { "Lord of the Flies", "flies.jpg", "William Golding", "Allegorical Fiction", "9780571200535", 1954, 4 },
                { "The Divine Comedy", "divine.jpg", "Dante Alighieri", "Epic Poetry", "9780140448955", 1320, 1 },
                { "The Stranger", "stranger.jpg", "Albert Camus", "Philosophical Fiction", "9780679720201", 1942, 3 },
                { "One Hundred Years of Solitude", "solitude.jpg", "Gabriel Garcia Marquez", "Magical Realism", "9780060883287", 1967, 4 },
                { "Don Quixote", "quixote.jpg", "Miguel de Cervantes", "Adventure", "9780060934347", 1605, 2 },
                { "The Picture of Dorian Gray", "dorian.jpg", "Oscar Wilde", "Gothic Fiction", "9780141439570", 1890, 3 },
                { "Anna Karenina", "anna.jpg", "Leo Tolstoy", "Romance", "9780143035008", 1877, 2 },
                { "The Sound and the Fury", "fury.jpg", "William Faulkner", "Southern Gothic", "9780679732242", 1929, 1 },
                { "Les Misérables", "lesmis.jpg", "Victor Hugo", "Historical Fiction", "9780451419439", 1862, 4 },
                { "The Grapes of Wrath", "grapes.jpg", "John Steinbeck", "Social Commentary", "9780143039433", 1939, 5 },
                { "Frankenstein", "frankenstein.jpg", "Mary Shelley", "Horror", "9780143131847", 1818, 3 },
                { "Wuthering Heights", "wuthering.jpg", "Emily Brontë", "Gothic Fiction", "9780141439556", 1847, 4 },
                { "The Old Man and the Sea", "oldman.jpg", "Ernest Hemingway", "Fiction", "9780684801223", 1952, 2 },
                { "Heart of Darkness", "heart.jpg", "Joseph Conrad", "Novella", "9780141441672", 1899, 3 },
                { "Dracula", "dracula.jpg", "Bram Stoker", "Gothic Fiction", "9780141439846", 1897, 4 },
                { "A Tale of Two Cities", "tale.jpg", "Charles Dickens", "Historical Fiction", "9780141439600", 1859, 2 },
                { "The Count of Monte Cristo", "count.jpg", "Alexandre Dumas", "Adventure", "9780140449266", 1844, 3 },
                { "Alice's Adventures in Wonderland", "alice.jpg", "Lewis Carroll", "Fantasy", "9780141439761", 1865, 5 },
                { "Fahrenheit 451", "fahrenheit.jpg", "Ray Bradbury", "Dystopian Fiction", "9781451673319", 1953, 4 },
                { "Beloved", "beloved.jpg", "Toni Morrison", "Historical Fiction", "9781400033416", 1987, 3 },
                { "Jane Eyre", "jane.jpg", "Charlotte Brontë", "Romance", "9780141441146", 1847, 2 },
                { "The Road", "road.jpg", "Cormac McCarthy", "Post-apocalyptic Fiction", "9780307387899", 2006, 4 },
                { "The Kite Runner", "kite.jpg", "Khaled Hosseini", "Drama", "9781594631931", 2003, 5 },
                { "A Clockwork Orange", "clockwork.jpg", "Anthony Burgess", "Science Fiction", "9780393312836", 1962, 3 },
                { "Slaughterhouse-Five", "slaughterhouse.jpg", "Kurt Vonnegut", "Science Fiction", "9780440180296", 1969, 2 },
                { "The Alchemist", "alchemist.jpg", "Paulo Coelho", "Fiction", "9780061122415", 1988, 5 },
                { "The Little Prince", "prince.jpg", "Antoine de Saint-Exupéry", "Fable", "9780156012195", 1943, 6 },
                { "Invisible Man", "invisible.jpg", "Ralph Ellison", "Fiction", "9780679732761", 1952, 2 },
                { "The Shining", "shining.jpg", "Stephen King", "Horror", "9780307743657", 1977, 4 },
                { "The Handmaid's Tale", "handmaid.jpg", "Margaret Atwood", "Dystopian Fiction", "9780385490818", 1985, 3 },
                { "The Giver", "giver.jpg", "Lois Lowry", "Young Adult Fiction", "9780544336261", 1993, 4 },
                { "Life of Pi", "pi.jpg", "Yann Martel", "Adventure", "9780156027328", 2001, 3 },
                { "The Secret Garden", "secret.jpg", "Frances Hodgson Burnett", "Children's Fiction", "9780141336503", 1911, 5 },
                { "The Hunger Games", "hunger.jpg", "Suzanne Collins", "Dystopian Fiction", "9780439023528", 2008, 7 },
                { "Harry Potter and the Philosopher's Stone", "hp1.jpg", "J.K. Rowling", "Fantasy", "9780747532699", 1997, 10 }
                }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
