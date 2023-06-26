using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P01_HospitalDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductsAddColumnDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedicament_Patients_PatientId1",
                table: "PatientMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedicament_PatientId1",
                table: "PatientMedicament");

            migrationBuilder.RenameColumn(
                name: "PatientId1",
                table: "PatientMedicament",
                newName: "PAtientId1");

            migrationBuilder.AlterColumn<int>(
                name: "PAtientId1",
                table: "PatientMedicament",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientMedicament",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament",
                column: "PAtientId1");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedicament_PatientId",
                table: "PatientMedicament",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedicament_Patients_PatientId",
                table: "PatientMedicament",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedicament_Patients_PatientId",
                table: "PatientMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedicament_PatientId",
                table: "PatientMedicament");

            migrationBuilder.RenameColumn(
                name: "PAtientId1",
                table: "PatientMedicament",
                newName: "PatientId1");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientMedicament",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId1",
                table: "PatientMedicament",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedicament_PatientId1",
                table: "PatientMedicament",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedicament_Patients_PatientId1",
                table: "PatientMedicament",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
