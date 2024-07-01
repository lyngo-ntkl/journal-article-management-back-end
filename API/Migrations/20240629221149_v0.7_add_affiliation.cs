using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class v07_add_affiliation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institution_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Affiliation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    InstitutionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Affiliation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Affiliation_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Affiliation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Afghanistan" },
                    { 2, "Albania" },
                    { 3, "Algeria" },
                    { 4, "Andorra" },
                    { 5, "Angola" },
                    { 6, "Antigua and Barbuda" },
                    { 7, "Argentina" },
                    { 8, "Armenia" },
                    { 9, "Australia" },
                    { 10, "Austria" },
                    { 11, "Azerbaijan" },
                    { 12, "Bahamas" },
                    { 13, "Bahrain" },
                    { 14, "Bangladesh" },
                    { 15, "Barbados" },
                    { 16, "Belarus" },
                    { 17, "Belgium" },
                    { 18, "Belize" },
                    { 19, "Benin" },
                    { 20, "Bhutan" },
                    { 21, "Bolivia" },
                    { 22, "Bosnia and Herzegovina" },
                    { 23, "Botswana" },
                    { 24, "Brazil" },
                    { 25, "Brunei" },
                    { 26, "Bulgaria" },
                    { 27, "Burkina Faso" },
                    { 28, "Burundi" },
                    { 29, "Cabo Verde" },
                    { 30, "Cambodia" },
                    { 31, "Cameroon" },
                    { 32, "Canada" },
                    { 33, "Central African Republic" },
                    { 34, "Chad" },
                    { 35, "Chile" },
                    { 36, "China" },
                    { 37, "Colombia" },
                    { 38, "Comoros" },
                    { 39, "Democratic Republic of the Congo" },
                    { 40, "Republic of the Congo" },
                    { 41, "Costa Rica" },
                    { 42, "Ivory Coast (Côte d’Ivoire)" },
                    { 43, "Croatia" },
                    { 44, "Cuba" },
                    { 45, "Cyprus" },
                    { 46, "Czech Republic" },
                    { 47, "Denmark" },
                    { 48, "Djibouti" },
                    { 49, "Dominica" },
                    { 50, "Dominican Republic" },
                    { 51, "East Timor (Timor-Leste)" },
                    { 52, "Ecuador" },
                    { 53, "Egypt" },
                    { 54, "El Salvador" },
                    { 55, "Equatorial Guinea" },
                    { 56, "Eritrea" },
                    { 57, "Estonia" },
                    { 58, "Eswatini" },
                    { 59, "Ethiopia" },
                    { 60, "Fiji" },
                    { 61, "Finland" },
                    { 62, "France" },
                    { 63, "Gabon" },
                    { 64, "Gambia" },
                    { 65, "Georgia" },
                    { 66, "Germany" },
                    { 67, "Ghana" },
                    { 68, "Greece" },
                    { 69, "Grenada" },
                    { 70, "Guatemala" },
                    { 71, "Guinea" },
                    { 72, "Guinea-Bissau" },
                    { 73, "Guyana" },
                    { 74, "Haiti" },
                    { 75, "Honduras" },
                    { 76, "Hungary" },
                    { 77, "Iceland" },
                    { 78, "India" },
                    { 79, "Indonesia" },
                    { 80, "Iran" },
                    { 81, "Iraq" },
                    { 82, "Ireland" },
                    { 83, "Israel" },
                    { 84, "Italy" },
                    { 85, "Jamaica" },
                    { 86, "Japan" },
                    { 87, "Jordan" },
                    { 88, "Kazakhstan" },
                    { 89, "Kenya" },
                    { 90, "Kiribati" },
                    { 91, "North Korea" },
                    { 92, "South Korea" },
                    { 93, "Kosovo" },
                    { 94, "Kuwait" },
                    { 95, "Kyrgyzstan" },
                    { 96, "Laos" },
                    { 97, "Latvia" },
                    { 98, "Lebanon" },
                    { 99, "Lesotho" },
                    { 100, "Liberia" },
                    { 101, "Libya" },
                    { 102, "Liechtenstein" },
                    { 103, "Lithuania" },
                    { 104, "Luxembourg" },
                    { 105, "Madagascar" },
                    { 106, "Malawi" },
                    { 107, "Malaysia" },
                    { 108, "Maldives" },
                    { 109, "Mali" },
                    { 110, "Malta" },
                    { 111, "Marshall Islands" },
                    { 112, "Mauritania" },
                    { 113, "Mauritius" },
                    { 114, "Mexico" },
                    { 115, "Federated States of Micronesia" },
                    { 116, "Moldova" },
                    { 117, "Monaco" },
                    { 118, "Mongolia" },
                    { 119, "Montenegro" },
                    { 120, "Morocco" },
                    { 121, "Mozambique" },
                    { 122, "Myanmar (Burma)" },
                    { 123, "Namibia" },
                    { 124, "Nauru" },
                    { 125, "Nepal" },
                    { 126, "Netherlands" },
                    { 127, "New Zealand" },
                    { 128, "Nicaragua" },
                    { 129, "Niger" },
                    { 130, "Nigeria" },
                    { 131, "North Macedonia" },
                    { 132, "Norway" },
                    { 133, "Oman" },
                    { 134, "Pakistan" },
                    { 135, "Palau" },
                    { 136, "Palestine" },
                    { 137, "Panama" },
                    { 138, "Papua New Guinea" },
                    { 139, "Paraguay" },
                    { 140, "Peru" },
                    { 141, "Philippines" },
                    { 142, "Poland" },
                    { 143, "Portugal" },
                    { 144, "Qatar" },
                    { 145, "Romania" },
                    { 146, "Russia" },
                    { 147, "Rwanda" },
                    { 148, "Saint Kitts and Nevis" },
                    { 149, "Saint Lucia" },
                    { 150, "Saint Vincent and the Grenadines" },
                    { 151, "Samoa" },
                    { 152, "San Marino" },
                    { 153, "Sao Tome and Principe" },
                    { 154, "Saudi Arabia" },
                    { 155, "Senegal" },
                    { 156, "Serbia" },
                    { 157, "Seychelles" },
                    { 158, "Sierra Leone" },
                    { 159, "Singapore" },
                    { 160, "Slovakia" },
                    { 161, "Slovenia" },
                    { 162, "Solomon Islands" },
                    { 163, "Somalia" },
                    { 164, "South Africa" },
                    { 165, "Spain" },
                    { 166, "Sri Lanka" },
                    { 167, "Sudan" },
                    { 168, "South Sudan" },
                    { 169, "Suriname" },
                    { 170, "Sweden" },
                    { 171, "Switzerland" },
                    { 172, "Syria" },
                    { 173, "Tajikistan" },
                    { 174, "Tanzania" },
                    { 175, "Thailand" },
                    { 176, "Togo" },
                    { 177, "Tonga" },
                    { 178, "Trinidad and Tobago" },
                    { 179, "Tunisia" },
                    { 180, "Turkey" },
                    { 181, "Turkmenistan" },
                    { 182, "Tuvalu" },
                    { 183, "Uganda" },
                    { 184, "Ukraine" },
                    { 185, "United Arab Emirates" },
                    { 186, "United Kingdom" },
                    { 187, "United States" },
                    { 188, "Uruguay" },
                    { 189, "Uzbekistan" },
                    { 190, "Vanuatu" },
                    { 191, "Vatican City" },
                    { 192, "Venezuela" },
                    { 193, "Vietnam" },
                    { 194, "Yemen" },
                    { 195, "Zambia" },
                    { 196, "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Biology" },
                    { 2, "Chemistry" },
                    { 3, "Physics" },
                    { 4, "Astronomy" },
                    { 5, "Geology" },
                    { 6, "Mathematics" },
                    { 7, "Computer Science" },
                    { 8, "Anthropology" },
                    { 9, "Economics" },
                    { 10, "Psychology" },
                    { 11, "Sociology" },
                    { 12, "Political Science" },
                    { 13, "English" },
                    { 14, "History" },
                    { 15, "Philosophy" },
                    { 16, "Art History" },
                    { 17, "Music" },
                    { 18, "Business Administration" },
                    { 19, "Law School" },
                    { 20, "Medical School" },
                    { 21, "Civil Engineering" },
                    { 22, "Electrical Engineering" },
                    { 23, "Software Engineering" },
                    { 24, "Nursing" },
                    { 25, "Education" },
                    { 26, "Architecture" },
                    { 27, "Journalism" },
                    { 28, "Public Health" },
                    { 29, "Environmental Science" },
                    { 30, "Agriculture" },
                    { 31, "Human Resources" },
                    { 32, "Support" },
                    { 33, "Research and Development" },
                    { 34, "Accounting" },
                    { 35, "Sales" },
                    { 36, "Business Development" },
                    { 37, "Marketing" },
                    { 38, "Legal" },
                    { 39, "Product Management" },
                    { 40, "Services" },
                    { 41, "Training" },
                    { 42, "Marine Science" },
                    { 43, "Meteorology" },
                    { 44, "Neuroscience" },
                    { 45, "Biochemistry" },
                    { 46, "Genetics" },
                    { 47, "Astrophysics" },
                    { 48, "Microbiology" },
                    { 49, "Optics" },
                    { 50, "Statistics" },
                    { 51, "Geography" },
                    { 52, "Criminology" },
                    { 53, "Social Work" },
                    { 54, "Urban Studies" },
                    { 55, "Public Policy" },
                    { 56, "Spanish" },
                    { 57, "French" },
                    { 58, "Chinese" },
                    { 59, "Arabic" },
                    { 60, "Mechanical Engineering" },
                    { 61, "Communication" },
                    { 62, "Gender Studies" },
                    { 63, "Cybersecurity" },
                    { 64, "Information Technology" },
                    { 65, "Roman Literature" },
                    { 66, "Greek Literature" },
                    { 67, "Religious Studies" },
                    { 69, "Linguistics" },
                    { 70, "Theatre" },
                    { 71, "Film Studies" },
                    { 72, "Comparative Literature" },
                    { 73, "Pharmacy" },
                    { 74, "Dentistry" },
                    { 75, "Veterinary Medicine" },
                    { 76, "Mathematics Education" },
                    { 77, "Science Education" },
                    { 78, "Foreign Language Education" },
                    { 79, "Physical Education" },
                    { 80, "Data Science" },
                    { 81, "Artificial Intelligence" },
                    { 82, "Management" },
                    { 83, "Finance" },
                    { 84, "International Relations" },
                    { 85, "Military Science" },
                    { 86, "Women's Studies" },
                    { 87, "African Studies" },
                    { 88, "Asian Studies" },
                    { 89, "African American Studies" },
                    { 90, "Latino Studies" },
                    { 91, "LGBTQ+ Studies" },
                    { 92, "Disability Studies" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "IsDeleted", "TopicName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Physics", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Mathematics", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Computer Science", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Chemistry", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Engineering", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Agriculture", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Medicine", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Pharmacology", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Biology", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Biotechnology", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Biomedical Application", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Geography", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Geology", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Geophysics", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Oceanography", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Ecology", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Climatology", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Nutrition", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Institution",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 9, "Australian Catholic University" },
                    { 2, 9, "Australian National University" },
                    { 3, 9, "Avondale University" },
                    { 4, 9, "Bond University" },
                    { 5, 9, "Central Queensland University" },
                    { 6, 9, "Charles Darwin University" },
                    { 7, 9, "Charles Sturt University" },
                    { 8, 9, "Curtin University" },
                    { 9, 9, "Deakin University" },
                    { 10, 9, "Edith Cowan University" },
                    { 11, 9, "Federation University Australia" },
                    { 12, 9, "Flinders University" },
                    { 13, 9, "Griffith University" },
                    { 14, 9, "James Cook University" },
                    { 15, 9, "La Trobe University" },
                    { 16, 9, "Macquarie University" },
                    { 17, 9, "Monash University" },
                    { 18, 9, "Murdoch University" },
                    { 19, 9, "Queensland University of Technology" },
                    { 20, 9, "RMIT University" },
                    { 21, 9, "Southern Cross University" },
                    { 22, 9, "Swinburne University of Technology" },
                    { 23, 9, "The University of Adelaide" },
                    { 24, 9, "The University of Melbourne" },
                    { 25, 9, "The University of Newcastle" },
                    { 26, 9, "The University of Notre Dame Australia" },
                    { 27, 9, "The University of Queensland" },
                    { 28, 9, "The University of Sydney" },
                    { 29, 9, "The University of Western Australia" },
                    { 30, 9, "Torrens University Australia" },
                    { 31, 9, "University of Canberra" },
                    { 32, 9, "University of Divinity" },
                    { 33, 9, "University of New England" },
                    { 34, 9, "University of New South Wales" },
                    { 35, 9, "University of South Australia" },
                    { 36, 9, "University of Southern Queensland" },
                    { 37, 9, "University of Tasmania" },
                    { 38, 9, "University of Technology Sydney" },
                    { 39, 9, "University of the Sunshine Coast" },
                    { 40, 9, "University of Wollongong" },
                    { 41, 9, "Victoria University" },
                    { 42, 193, "Hanoi University of Science and Technology" },
                    { 43, 193, "School of Information and Communication Technology" },
                    { 44, 193, "School of Material Science" },
                    { 45, 193, "School of Mechanical Engineering" },
                    { 46, 193, "School of Chemistry and Life Science" },
                    { 47, 193, "School of Electrical and Electronic Engineering" },
                    { 48, 193, "University of Economics Ho Chi Minh City" },
                    { 49, 193, "College of Business" },
                    { 50, 193, "College of Economics, Law and Gorvernment" },
                    { 51, 193, "College of Technology and Design" },
                    { 52, 193, "Vietnam National University, Hanoi" },
                    { 53, 193, "University of Engineering and Technology" },
                    { 54, 193, "University of Sciences" },
                    { 55, 193, "University of Social Sciences and Humanities" },
                    { 56, 193, "University of Economics and Business" },
                    { 57, 193, "University of Medicine and Pharmacy" },
                    { 58, 193, "University of Law" },
                    { 59, 193, "Vietnam–Japan University" },
                    { 60, 193, "International School" },
                    { 61, 193, "School of Business and Management" },
                    { 62, 193, "School of Interdisciplinary Sciences" },
                    { 63, 193, "Vietnam National University, Ho Chi Minh City" },
                    { 64, 193, "University of Technology" },
                    { 65, 193, "University of Science" },
                    { 66, 193, "Ho Chi Minh City University of Technology and Education" },
                    { 67, 193, "International University" },
                    { 68, 193, "University of Economics and Law" },
                    { 69, 193, "University of Information Technology" },
                    { 70, 193, "School of Medicine" },
                    { 71, 193, "School of Political and Administration Sciences" },
                    { 72, 193, "Institute of Environment and Resource" },
                    { 73, 193, "Foreign Trade University" },
                    { 74, 193, "Hanoi University of Mining and Geology" },
                    { 75, 193, "Hanoi National University of Education" },
                    { 76, 193, "Hanoi University of Industrial Fine Arts" },
                    { 77, 193, "National Academy of Education Management" },
                    { 78, 193, "University of Transportation and Communication" },
                    { 79, 193, "National Economics University" },
                    { 80, 193, "University of Education" },
                    { 81, 193, "University of Languages and International Studies" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affiliation_DepartmentId",
                table: "Affiliation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Affiliation_InstitutionId",
                table: "Affiliation",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Affiliation_UserId",
                table: "Affiliation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Country",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_CountryId",
                table: "Institution",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Institution_Name_CountryId",
                table: "Institution",
                columns: new[] { "Name", "CountryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Affiliation");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
