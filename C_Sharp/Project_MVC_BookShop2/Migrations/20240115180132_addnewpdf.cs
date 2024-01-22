using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMVCBookShop2.Migrations
{
    /// <inheritdoc />
    public partial class addnewpdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookPdfUrl",
                table: "Books2",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookPdfUrl",
                table: "Books2");
        }
    }
}
