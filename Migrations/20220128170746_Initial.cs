using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AVPatients.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "concentrationUnit",
                columns: table => new
                {
                    concentrationCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_concentrationUnit", x => x.concentrationCode);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    countryCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false, comment: "2-character short form for country"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "formal name of country"),
                    postalPattern = table.Column<string>(unicode: false, maxLength: 120, nullable: true, comment: "regular expression used to validate the postal or zip code for this country, includes ^ and $"),
                    phonePattern = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "regular expression used to validate a phone number in this country, includes ^ and $"),
                    federalSalesTax = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.countryCode);
                },
                comment: "list of countries and data pertinent to them");

            migrationBuilder.CreateTable(
                name: "diagnosisCategory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false, comment: "random key, allowing category to be renamed")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "major medical categories: cardiology, respiratory, etc.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaacategory_PK", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "dispensingUnit",
                columns: table => new
                {
                    dispensingCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispensingUnit", x => x.dispensingCode);
                });

            migrationBuilder.CreateTable(
                name: "medicationType",
                columns: table => new
                {
                    medicationTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicationType", x => x.medicationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "province",
                columns: table => new
                {
                    provinceCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false, comment: "2-character province code ... ON, BC, etc"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "full province name ... Ontario, etc."),
                    countryCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    salesTaxCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    salesTax = table.Column<double>(nullable: false),
                    includesFederalTax = table.Column<bool>(nullable: false),
                    firstPostalLetter = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaaprovince_PK", x => x.provinceCode)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_province_country",
                        column: x => x.countryCode,
                        principalTable: "country",
                        principalColumn: "countryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "diagnosis",
                columns: table => new
                {
                    diagnosisId = table.Column<int>(nullable: false, comment: "random key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "medical name for ailment"),
                    diagnosisCategoryId = table.Column<int>(nullable: false, comment: "link to major categories")
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaadiagnosis_PK", x => x.diagnosisId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_diagnosis_diagnosisCategory",
                        column: x => x.diagnosisCategoryId,
                        principalTable: "diagnosisCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medication",
                columns: table => new
                {
                    din = table.Column<string>(unicode: false, maxLength: 8, nullable: false, comment: "8-digit drug identification number"),
                    name = table.Column<string>(maxLength: 20, nullable: false, comment: "name of drug as branded by manufacturer"),
                    image = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "file-name of product image"),
                    medicationTypeId = table.Column<int>(nullable: false, comment: "type of drug ... anticoagulant, antihistimine, etc."),
                    dispensingCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "dispensing units: pills, capsils, mg, tablespoons, etc."),
                    concentration = table.Column<double>(nullable: false, comment: "concentration quantity, n concentration units, zero if n/a"),
                    concentrationCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaamedication_PK", x => x.din)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_medication_concentrationUnit",
                        column: x => x.concentrationCode,
                        principalTable: "concentrationUnit",
                        principalColumn: "concentrationCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_medication_dispensingUnit",
                        column: x => x.dispensingCode,
                        principalTable: "dispensingUnit",
                        principalColumn: "dispensingCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_medication_medicationType",
                        column: x => x.medicationTypeId,
                        principalTable: "medicationType",
                        principalColumn: "medicationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    patientId = table.Column<int>(nullable: false, comment: "random patient number")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "patient's first or given name"),
                    lastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "patient's surname or family name"),
                    address = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "street address"),
                    city = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "city"),
                    provinceCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: true, comment: "2-character province code"),
                    postalCode = table.Column<string>(unicode: false, maxLength: 10, nullable: true, comment: "postal code: A9A 9A9"),
                    OHIP = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "12-character provincial medical"),
                    dateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true, comment: "date of birth"),
                    deceased = table.Column<bool>(nullable: false, comment: "if yes, date of death required else, ignore date of death"),
                    dateOfDeath = table.Column<DateTime>(type: "datetime", nullable: true, comment: "date of death (null if alive)"),
                    homePhone = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "10-digit home phone number"),
                    gender = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: true, comment: "M or F")
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaapatient_PK", x => x.patientId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_patient_province",
                        column: x => x.provinceCode,
                        principalTable: "province",
                        principalColumn: "provinceCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "treatment",
                columns: table => new
                {
                    treatmentId = table.Column<int>(nullable: false, comment: "random key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false, comment: "formal name of the procedure"),
                    description = table.Column<string>(unicode: false, nullable: true, comment: "free-form decription of the procedure"),
                    diagnosisId = table.Column<int>(nullable: false, comment: "link back to diagnosis")
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaatreatment_PK", x => x.treatmentId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_treatment_diagnosis",
                        column: x => x.diagnosisId,
                        principalTable: "diagnosis",
                        principalColumn: "diagnosisId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patientDiagnosis",
                columns: table => new
                {
                    patientDiagnosisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientId = table.Column<int>(nullable: false),
                    diagnosisId = table.Column<int>(nullable: false),
                    comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientDiagnosis", x => x.patientDiagnosisId);
                    table.ForeignKey(
                        name: "FK_patientDiagnosis_diagnosis",
                        column: x => x.diagnosisId,
                        principalTable: "diagnosis",
                        principalColumn: "diagnosisId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patientDiagnosis_patient",
                        column: x => x.patientId,
                        principalTable: "patient",
                        principalColumn: "patientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "treatmentMedication",
                columns: table => new
                {
                    treatmentId = table.Column<int>(nullable: false, comment: "link to treatment this record is for"),
                    din = table.Column<string>(unicode: false, maxLength: 8, nullable: false, comment: "link to medication for this treatment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaatreatmentMedication_PK", x => new { x.treatmentId, x.din })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_treatmentMedication_medication",
                        column: x => x.din,
                        principalTable: "medication",
                        principalColumn: "din",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_treatmentMedication_treatment",
                        column: x => x.treatmentId,
                        principalTable: "treatment",
                        principalColumn: "treatmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patientTreatment",
                columns: table => new
                {
                    patientTreatmentId = table.Column<int>(nullable: false, comment: "random key for treatment on this patient")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    treatmentId = table.Column<int>(nullable: false, comment: "link back to treatment"),
                    datePrescribed = table.Column<DateTime>(type: "datetime", nullable: false, comment: "date treatment prescribed to patient"),
                    comments = table.Column<string>(unicode: false, nullable: true, comment: "general free-form comments about treatment"),
                    patientDiagnosisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaapatientTreatment_PK", x => x.patientTreatmentId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_patientTreatment_patientDiagnosis",
                        column: x => x.patientDiagnosisId,
                        principalTable: "patientDiagnosis",
                        principalColumn: "patientDiagnosisId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "patientTreatment_FK01",
                        column: x => x.treatmentId,
                        principalTable: "treatment",
                        principalColumn: "treatmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patientMedication",
                columns: table => new
                {
                    patientMedicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientTreatmentId = table.Column<int>(nullable: false, comment: "link back to the procedure for this patient"),
                    din = table.Column<string>(unicode: false, maxLength: 8, nullable: false, comment: "link to medication by drug identification number"),
                    dose = table.Column<double>(nullable: true, defaultValueSql: "((0))", comment: "number of dispensing units at a time"),
                    frequency = table.Column<int>(nullable: true, defaultValueSql: "((0))", comment: "number of doses per day/week/month; zero if as-required"),
                    frequencyPeriod = table.Column<string>(unicode: false, maxLength: 50, nullable: true, comment: "period of frequency: per day, week, month or as-required"),
                    exactMinMax = table.Column<string>(unicode: false, maxLength: 50, nullable: true, defaultValueSql: "(N'exact')", comment: "dosage frequency is exactly x periods, minimum of, or maximum of"),
                    comments = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("aaaaapatientMedication_PK", x => x.patientMedicationId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_patientMedication_medication",
                        column: x => x.din,
                        principalTable: "medication",
                        principalColumn: "din",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_patientMedication_patientTreatment",
                        column: x => x.patientTreatmentId,
                        principalTable: "patientTreatment",
                        principalColumn: "patientTreatmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "diseasecategoryId",
                table: "diagnosis",
                column: "diagnosisCategoryId");

            migrationBuilder.CreateIndex(
                name: "ailmentId",
                table: "diagnosis",
                column: "diagnosisId");

            migrationBuilder.CreateIndex(
                name: "categoryId",
                table: "diagnosisCategory",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_medication_concentrationCode",
                table: "medication",
                column: "concentrationCode");

            migrationBuilder.CreateIndex(
                name: "IX_medication_dispensingCode",
                table: "medication",
                column: "dispensingCode");

            migrationBuilder.CreateIndex(
                name: "IX_medication_medicationTypeId",
                table: "medication",
                column: "medicationTypeId");

            migrationBuilder.CreateIndex(
                name: "homePhone",
                table: "patient",
                column: "homePhone");

            migrationBuilder.CreateIndex(
                name: "patientId",
                table: "patient",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "postalCode",
                table: "patient",
                column: "postalCode");

            migrationBuilder.CreateIndex(
                name: "provincepatient",
                table: "patient",
                column: "provinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_patientDiagnosis_diagnosisId",
                table: "patientDiagnosis",
                column: "diagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_patientDiagnosis_patientId",
                table: "patientDiagnosis",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "medicationpatientMedication",
                table: "patientMedication",
                column: "din");

            migrationBuilder.CreateIndex(
                name: "patientTreatmentpatientMedication",
                table: "patientMedication",
                column: "patientTreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_patientTreatment_patientDiagnosisId",
                table: "patientTreatment",
                column: "patientDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "patientProcedureId",
                table: "patientTreatment",
                column: "patientTreatmentId");

            migrationBuilder.CreateIndex(
                name: "procedurepatientProcedure",
                table: "patientTreatment",
                column: "treatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_province_countryCode",
                table: "province",
                column: "countryCode");

            migrationBuilder.CreateIndex(
                name: "ProvinceCode",
                table: "province",
                column: "provinceCode");

            migrationBuilder.CreateIndex(
                name: "diagnosisprocedure",
                table: "treatment",
                column: "diagnosisId");

            migrationBuilder.CreateIndex(
                name: "procedureId",
                table: "treatment",
                column: "treatmentId");

            migrationBuilder.CreateIndex(
                name: "medicationtreatmentMedication",
                table: "treatmentMedication",
                column: "din");

            migrationBuilder.CreateIndex(
                name: "treatmenttreatmentMedication",
                table: "treatmentMedication",
                column: "treatmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patientMedication");

            migrationBuilder.DropTable(
                name: "treatmentMedication");

            migrationBuilder.DropTable(
                name: "patientTreatment");

            migrationBuilder.DropTable(
                name: "medication");

            migrationBuilder.DropTable(
                name: "patientDiagnosis");

            migrationBuilder.DropTable(
                name: "treatment");

            migrationBuilder.DropTable(
                name: "concentrationUnit");

            migrationBuilder.DropTable(
                name: "dispensingUnit");

            migrationBuilder.DropTable(
                name: "medicationType");

            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.DropTable(
                name: "diagnosis");

            migrationBuilder.DropTable(
                name: "province");

            migrationBuilder.DropTable(
                name: "diagnosisCategory");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
