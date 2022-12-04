using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tikitapp.Website.Migrations {
	/// <inheritdoc />
	public partial class AddSlugToArtist : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<string>(
				name: "Slug",
				table: "Artists",
				type: "varchar(100)",
				unicode: false,
				maxLength: 100,
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateIndex(
				name: "IX_Artists_Slug",
				table: "Artists",
				column: "Slug");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropIndex(
				name: "IX_Artists_Slug",
				table: "Artists");

			migrationBuilder.DropColumn(
				name: "Slug",
				table: "Artists");
		}
	}
}
