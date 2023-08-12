using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStoreAPI.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Drama" },
                    // Add more genres as needed
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "MovieId", "Title", "ReleaseTime", "MovieImage", "Cast", "Director" },
                values: new object[,]
                {
                    { 1, "Movie 1", new DateTime(2023, 8, 1), "image1.jpg", "Actor A, Actress B", "Director X" },
                    { 2, "Movie 2", new DateTime(2023, 8, 15), "image2.jpg", "Actor C, Actress D", "Director Y" },
                    // Add more movies as needed
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
