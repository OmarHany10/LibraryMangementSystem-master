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
                   {"The Great Gatsby " , "great.jpg " , "F. Scott Fitzgerald " ,  "Classic Fiction " ,"9780743273565 " ,  1925 ,  3  },
                   {"1984 " , "1984.jpg " , "George Orwell " ,  "Dystopian Fiction " , "9780451524935 " ,  1949 ,  5  },
                   {"To Kill a Mockingbird " , "tokill.jpg " ,"Harper Lee " ,  "Classic Fiction " , "9780060935467 " ,  1960 ,  4  },

                }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
