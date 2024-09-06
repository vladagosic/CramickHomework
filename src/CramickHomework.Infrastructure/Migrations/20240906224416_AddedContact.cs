using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CramickHomework.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    CreatedById = table.Column<string>(type: "varchar(36)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    UpdatedById = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6e0da5cf-14f0-4f25-8785-8715f608756b"),
                column: "ConcurrencyStamp",
                value: "1c5fdb75-2fe8-4aca-95ab-dc2f89fcee37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b920cbd7-d17e-44b0-82c9-89c7ed51d7a8"),
                column: "ConcurrencyStamp",
                value: "44603441-6468-49c6-9ef4-78a2bb7e11cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b59b00e0-d70e-499f-bfaf-dca40561fa65",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "0e269ab7-6437-4a53-b032-66a9ee9bf8a6", new DateTimeOffset(new DateTime(2024, 9, 7, 0, 44, 15, 897, DateTimeKind.Unspecified).AddTicks(8831), new TimeSpan(0, 2, 0, 0, 0)), "F7851428.0DAE.470D.AC89.60F7C61A065B", "AQAAAAIAAYagAAAAEP5vtprtgSPuj/Gx4HHOfbiKSZTcNVsyzpe1mhXNJh7leca45E92jQ2YDc7Mmpg4VQ==", "0572e783-b426-442b-b394-43c847b1eb24", "f7851428.0dae.470d.ac89.60f7c61a065b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8d598de-6e02-482f-bea6-c7e0b0c6ea7c",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8e37e441-7f8e-456e-8b5d-0057b4e540d1", new DateTimeOffset(new DateTime(2024, 9, 7, 0, 44, 15, 968, DateTimeKind.Unspecified).AddTicks(7687), new TimeSpan(0, 2, 0, 0, 0)), "390E935F.7B61.4863.9919.43855DA11ACF", "AQAAAAIAAYagAAAAEJqBbnKAgYBjPFHgbAmCLbm9+wV3ezY+o1vMF9osNX47WQDNzD4SipE8oBqXNs1RVg==", "7ed522af-c133-4423-9904-b85ad07c16d8", "390e935f.7b61.4863.9919.43855da11acf" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CreatedById",
                table: "Contact",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UpdatedById",
                table: "Contact",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6e0da5cf-14f0-4f25-8785-8715f608756b"),
                column: "ConcurrencyStamp",
                value: "f8835e4d-0b5f-4e38-825b-b10440daf469");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b920cbd7-d17e-44b0-82c9-89c7ed51d7a8"),
                column: "ConcurrencyStamp",
                value: "7b1ac459-4632-4491-b7cb-a838300f19ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b59b00e0-d70e-499f-bfaf-dca40561fa65",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b7f98d94-6fa8-47c7-9273-2b91b0357416", new DateTimeOffset(new DateTime(2024, 9, 6, 23, 26, 36, 843, DateTimeKind.Unspecified).AddTicks(1966), new TimeSpan(0, 2, 0, 0, 0)), "C38FF139.CDF3.47FE.ACD0.A1D15EE45469", "AQAAAAIAAYagAAAAEP7tqu3ho4+ODzdL8L3t6dNQ7VSDmK5Bz52FpdNFJFEOfao3BdeXJZ6PL4RhGroykA==", "8496fbf0-607e-4924-beb8-36a47a446b92", "c38ff139.cdf3.47fe.acd0.a1d15ee45469" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8d598de-6e02-482f-bea6-c7e0b0c6ea7c",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "33cb3d2d-cc0e-4ce1-b26f-104d4a07e42f", new DateTimeOffset(new DateTime(2024, 9, 6, 23, 26, 36, 951, DateTimeKind.Unspecified).AddTicks(139), new TimeSpan(0, 2, 0, 0, 0)), "9414AE0F.9177.4B34.B110.30821820CBBC", "AQAAAAIAAYagAAAAEOnkZajE/j/EQY0ppDmh7iE4rFrIduYoh7fdYmb2FhsBGbm5Qo+eSveusXxxuc3GIA==", "c347593b-60f1-4b0b-afb5-99553eddbc16", "9414ae0f.9177.4b34.b110.30821820cbbc" });
        }
    }
}
