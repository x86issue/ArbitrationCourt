using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AC.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arbitrators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Exp = table.Column<int>(type: "integer", nullable: true),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arbitrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defendants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defendants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plaintiffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plaintiffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PlaintiffId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefendantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArbitratorId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Arbitrators_ArbitratorId",
                        column: x => x.ArbitratorId,
                        principalTable: "Arbitrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Cases_Defendants_DefendantId",
                        column: x => x.DefendantId,
                        principalTable: "Defendants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Plaintiffs_PlaintiffId",
                        column: x => x.PlaintiffId,
                        principalTable: "Plaintiffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseCourtRules",
                columns: table => new
                {
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourtRuleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseCourtRules", x => new { x.CaseId, x.CourtRuleId });
                    table.ForeignKey(
                        name: "FK_CaseCourtRules_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseCourtRules_CourtRules_CourtRuleId",
                        column: x => x.CourtRuleId,
                        principalTable: "CourtRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PlaintiffId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefendantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Defendants_DefendantId",
                        column: x => x.DefendantId,
                        principalTable: "Defendants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Plaintiffs_PlaintiffId",
                        column: x => x.PlaintiffId,
                        principalTable: "Plaintiffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementProposals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefendantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementProposals_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementProposals_Defendants_DefendantId",
                        column: x => x.DefendantId,
                        principalTable: "Defendants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Verdicts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArbitratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verdicts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verdicts_Arbitrators_ArbitratorId",
                        column: x => x.ArbitratorId,
                        principalTable: "Arbitrators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verdicts_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseCourtRules_CourtRuleId",
                table: "CaseCourtRules",
                column: "CourtRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ArbitratorId",
                table: "Cases",
                column: "ArbitratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DefendantId",
                table: "Cases",
                column: "DefendantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PlaintiffId",
                table: "Cases",
                column: "PlaintiffId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CaseId",
                table: "Claims",
                column: "CaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_DefendantId",
                table: "Claims",
                column: "DefendantId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PlaintiffId",
                table: "Claims",
                column: "PlaintiffId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CaseId",
                table: "Comments",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementProposals_CaseId",
                table: "SettlementProposals",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementProposals_DefendantId",
                table: "SettlementProposals",
                column: "DefendantId");

            migrationBuilder.CreateIndex(
                name: "IX_Verdicts_ArbitratorId",
                table: "Verdicts",
                column: "ArbitratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Verdicts_CaseId",
                table: "Verdicts",
                column: "CaseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseCourtRules");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SettlementProposals");

            migrationBuilder.DropTable(
                name: "Verdicts");

            migrationBuilder.DropTable(
                name: "CourtRules");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Arbitrators");

            migrationBuilder.DropTable(
                name: "Defendants");

            migrationBuilder.DropTable(
                name: "Plaintiffs");
        }
    }
}
