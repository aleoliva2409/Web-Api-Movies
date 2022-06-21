using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIMovies.Migrations
{
    public partial class ChangeTypeScoreReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Score",
                table: "Reviews",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d",
                column: "ConcurrencyStamp",
                value: "f031f67f-6d8a-4eb0-a03d-b2f2678241bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5673b8cf-12de-44f6-92ad-fae4a77932ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b7afc88-fb6b-4200-9530-c04a8d564c76", "AQAAAAEAACcQAAAAEBHyCWriWNBEjf4SumxDyWXr8RiFnliLvzNfM51g7wgojTZPjh2qf13+Im4Eiy6lsQ==", "c29c7e26-6f07-4f1e-a55c-45d9d210dde3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d",
                column: "ConcurrencyStamp",
                value: "2beba73c-3016-4510-b633-f885644f9dd0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5673b8cf-12de-44f6-92ad-fae4a77932ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6484c1f9-7fce-408e-a114-52bdf5826d5c", "AQAAAAEAACcQAAAAEP44P0eXd97Sxm1lYughq8wsX9/zNeUSBqXkCzoTAbSl8wFeR+D437wqACH0dzY2Aw==", "493a5529-3197-4a72-95a8-cc99e6797f66" });
        }
    }
}
