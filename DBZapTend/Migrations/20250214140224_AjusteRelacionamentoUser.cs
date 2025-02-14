using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBZapTend.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mydb");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "mydb",
                columns: table => new
                {
                    idCategory = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Category_pkey", x => x.idCategory);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                schema: "mydb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Annually = table.Column<string>(type: "text", nullable: true),
                    Monthly = table.Column<string>(type: "text", nullable: true),
                    AmountInstance = table.Column<int>(type: "integer", nullable: false),
                    DiscountAnnually = table.Column<string>(type: "text", nullable: true),
                    DiscountMonthly = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Plan_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nichos",
                schema: "mydb",
                columns: table => new
                {
                    idNichos = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Category_idCategory = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Nichos_pkey", x => x.idNichos);
                    table.ForeignKey(
                        name: "fk_Nichos_Category1",
                        column: x => x.Category_idCategory,
                        principalSchema: "mydb",
                        principalTable: "Category",
                        principalColumn: "idCategory");
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "mydb",
                columns: table => new
                {
                    IdAutentication = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    CpfCnpj = table.Column<string>(type: "text", nullable: true),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    Telephone = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_IdAutentication_pkey", x => x.IdAutentication);
                    table.ForeignKey(
                        name: "FK_User_Plan_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "mydb",
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prompts",
                schema: "mydb",
                columns: table => new
                {
                    idPrompts = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    Nichos_idNichos = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Prompts_pkey", x => x.idPrompts);
                    table.ForeignKey(
                        name: "fk_Prompts_Nichos1",
                        column: x => x.Nichos_idNichos,
                        principalSchema: "mydb",
                        principalTable: "Nichos",
                        principalColumn: "idNichos");
                });

            migrationBuilder.CreateTable(
                name: "Instance",
                schema: "mydb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    user_iduser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Instance_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "fk_instance_user",
                        column: x => x.user_iduser,
                        principalSchema: "mydb",
                        principalTable: "User",
                        principalColumn: "IdAutentication");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "mydb",
                columns: table => new
                {
                    idPayments = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypePayment = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    ValuePayment = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    User_Id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cycle = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Plan_Id = table.Column<int>(type: "integer", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Payments_pkey", x => x.idPayments);
                    table.ForeignKey(
                        name: "FK_Payments_Plan_Plan_Id",
                        column: x => x.Plan_Id,
                        principalSchema: "mydb",
                        principalTable: "Plan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_Payments_User1",
                        column: x => x.User_Id,
                        principalSchema: "mydb",
                        principalTable: "User",
                        principalColumn: "IdAutentication");
                });

            migrationBuilder.CreateTable(
                name: "User_Nichos",
                schema: "mydb",
                columns: table => new
                {
                    idUser_Nichos = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Nichos_idNichos = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_Nichos_pkey", x => x.idUser_Nichos);
                    table.ForeignKey(
                        name: "fk_User_Nichos_Nichos1",
                        column: x => x.Nichos_idNichos,
                        principalSchema: "mydb",
                        principalTable: "Nichos",
                        principalColumn: "idNichos");
                    table.ForeignKey(
                        name: "fk_User_Nichos_User1",
                        column: x => x.User_Id,
                        principalSchema: "mydb",
                        principalTable: "User",
                        principalColumn: "IdAutentication");
                });

            migrationBuilder.CreateTable(
                name: "Variaveis",
                schema: "mydb",
                columns: table => new
                {
                    idVariaveis = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Prompts_idPrompts = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Variaveis_pkey", x => x.idVariaveis);
                    table.ForeignKey(
                        name: "fk_Variaveis_Prompts1",
                        column: x => x.Prompts_idPrompts,
                        principalSchema: "mydb",
                        principalTable: "Prompts",
                        principalColumn: "idPrompts");
                });

            migrationBuilder.CreateTable(
                name: "Valores_Variaveis",
                schema: "mydb",
                columns: table => new
                {
                    idValores_Variaveis = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Prompts_idPrompts = table.Column<int>(type: "integer", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Variaveis_idVariaveis = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Valores_Variaveis_pkey", x => x.idValores_Variaveis);
                    table.ForeignKey(
                        name: "fk_Valores_Variaveis_Prompts1",
                        column: x => x.Prompts_idPrompts,
                        principalSchema: "mydb",
                        principalTable: "Prompts",
                        principalColumn: "idPrompts");
                    table.ForeignKey(
                        name: "fk_Valores_Variaveis_User1",
                        column: x => x.User_Id,
                        principalSchema: "mydb",
                        principalTable: "User",
                        principalColumn: "IdAutentication");
                    table.ForeignKey(
                        name: "fk_Valores_Variaveis_Variaveis1",
                        column: x => x.Variaveis_idVariaveis,
                        principalSchema: "mydb",
                        principalTable: "Variaveis",
                        principalColumn: "idVariaveis");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instance_user_iduser",
                schema: "mydb",
                table: "Instance",
                column: "user_iduser");

            migrationBuilder.CreateIndex(
                name: "IX_Nichos_Category_idCategory",
                schema: "mydb",
                table: "Nichos",
                column: "Category_idCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Plan_Id",
                schema: "mydb",
                table: "Payments",
                column: "Plan_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_User_Id",
                schema: "mydb",
                table: "Payments",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_Nichos_idNichos",
                schema: "mydb",
                table: "Prompts",
                column: "Nichos_idNichos");

            migrationBuilder.CreateIndex(
                name: "IX_User_PlanId",
                schema: "mydb",
                table: "User",
                column: "PlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "User_IdAutentication_unique",
                schema: "mydb",
                table: "User",
                column: "IdAutentication",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Nichos_Nichos_idNichos",
                schema: "mydb",
                table: "User_Nichos",
                column: "Nichos_idNichos");

            migrationBuilder.CreateIndex(
                name: "IX_User_Nichos_User_Id",
                schema: "mydb",
                table: "User_Nichos",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Valores_Variaveis_Prompts_idPrompts",
                schema: "mydb",
                table: "Valores_Variaveis",
                column: "Prompts_idPrompts");

            migrationBuilder.CreateIndex(
                name: "IX_Valores_Variaveis_User_Id",
                schema: "mydb",
                table: "Valores_Variaveis",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Valores_Variaveis_Variaveis_idVariaveis",
                schema: "mydb",
                table: "Valores_Variaveis",
                column: "Variaveis_idVariaveis");

            migrationBuilder.CreateIndex(
                name: "IX_Variaveis_Prompts_idPrompts",
                schema: "mydb",
                table: "Variaveis",
                column: "Prompts_idPrompts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instance",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "User_Nichos",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Valores_Variaveis",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "User",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Variaveis",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Plan",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Prompts",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Nichos",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "mydb");
        }
    }
}
