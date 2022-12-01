using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tikitapp.Website.Migrations {
	/// <inheritdoc />
	public partial class InsertSampleArtistRecords : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.Sql(@"
				INSERT INTO Artists(Id, Name) VALUES('00000001-0000-0000-0000-000BDA4C94B5', 'Alter Column')
				INSERT INTO Artists(Id, Name) VALUES('00000002-0000-0000-0000-000BDA4C94B5', 'Binary Search')
				INSERT INTO Artists(Id, Name) VALUES('00000003-0000-0000-0000-000BDA4C94B5', 'C0DA')
				INSERT INTO Artists(Id, Name) VALUES('00000004-0000-0000-0000-000BDA4C94B5', 'Dev Leppard')
				INSERT INTO Artists(Id, Name) VALUES('00000005-0000-0000-0000-000BDA4C94B5', 'Erlangst')
				INSERT INTO Artists(Id, Name) VALUES('00000006-0000-0000-0000-000BDA4C94B5', 'Floating Point Foxes')
				INSERT INTO Artists(Id, Name) VALUES('00000007-0000-0000-0000-000BDA4C94B5', 'GOTO 10')
				INSERT INTO Artists(Id, Name) VALUES('00000008-0000-0000-0000-000BDA4C94B5', 'Haskell''s Angels')
				INSERT INTO Artists(Id, Name) VALUES('00000009-0000-0000-0000-000BDA4C94B5', 'Iron Median')
				INSERT INTO Artists(Id, Name) VALUES('0000000A-0000-0000-0000-000BDA4C94B5', 'Java''s Crypt')
				INSERT INTO Artists(Id, Name) VALUES('0000000B-0000-0000-0000-000BDA4C94B5', 'Killerbyte')
				INSERT INTO Artists(Id, Name) VALUES('0000000C-0000-0000-0000-000BDA4C94B5', 'Lambda of God')
				INSERT INTO Artists(Id, Name) VALUES('0000000D-0000-0000-0000-000BDA4C94B5', 'Null Terminated String Band')
				INSERT INTO Artists(Id, Name) VALUES('0000000E-0000-0000-0000-000BDA4C94B5', 'Mötherböard')
				INSERT INTO Artists(Id, Name) VALUES('0000000F-0000-0000-0000-000BDA4C94B5', 'Överflow')
				INSERT INTO Artists(Id, Name) VALUES('00000010-0000-0000-0000-000BDA4C94B5', 'Pascal''s Wager')
				INSERT INTO Artists(Id, Name) VALUES('00000011-0000-0000-0000-000BDA4C94B5', 'Quantum Gate')
				INSERT INTO Artists(Id, Name) VALUES('00000012-0000-0000-0000-000BDA4C94B5', 'RUN CMD')
				INSERT INTO Artists(Id, Name) VALUES('00000013-0000-0000-0000-000BDA4C94B5', 'Script Kiddies')
				INSERT INTO Artists(Id, Name) VALUES('00000014-0000-0000-0000-000BDA4C94B5', 'Tail Recursion')
				INSERT INTO Artists(Id, Name) VALUES('00000015-0000-0000-0000-000BDA4C94B5', 'Unsigned Integers')
				INSERT INTO Artists(Id, Name) VALUES('00000016-0000-0000-0000-000BDA4C94B5', 'Virtual Machine')
				INSERT INTO Artists(Id, Name) VALUES('00000017-0000-0000-0000-000BDA4C94B5', 'Webmaster of Puppets')
				INSERT INTO Artists(Id, Name) VALUES('00000018-0000-0000-0000-000BDA4C94B5', 'XSLTE')
				INSERT INTO Artists(Id, Name) VALUES('00000019-0000-0000-0000-000BDA4C94B5', 'The Y Combinators')
				INSERT INTO Artists(Id, Name) VALUES('0000001A-0000-0000-0000-000BDA4C94B5', 'Zero Based Index')
			");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {

		}
	}
}
