using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations.Pg
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    AnnouncementCategory = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Doc = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesDocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageTemplate = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Exception = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Provider = table.Column<int>(type: "integer", nullable: false),
                    ExternalUserId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Code = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsSend = table.Column<bool>(type: "boolean", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    SubscriptionMonth = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonFullName = table.Column<string>(type: "text", nullable: true),
                    PersonPhone = table.Column<string>(type: "text", nullable: true),
                    PersonSocialMediaLink = table.Column<string>(type: "text", nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Channel = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CitizenId = table.Column<long>(type: "bigint", nullable: true),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MobilePhones = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    YoutubeProfile = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    InstagramProfile = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SpotifyProfile = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VideoOrSoundUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MusicStyle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Manager = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Studio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    InspirationArtists = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Biography = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateContactDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Translates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LanguagesId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translates_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupClaims",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    ClaimId = table.Column<int>(type: "integer", nullable: false),
                    OperationClaimId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupClaims", x => new { x.GroupId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_GroupClaims_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Doc = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuponCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    DiscountPercent = table.Column<double>(type: "double precision", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuponCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuponCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StageDiscipline = table.Column<string>(type: "text", nullable: true),
                    TotalAudience = table.Column<string>(type: "text", nullable: true),
                    ImageManagement = table.Column<string>(type: "text", nullable: true),
                    BookingPotential = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Doc = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Tc = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MerchantOid = table.Column<string>(type: "text", nullable: true),
                    UserIp = table.Column<string>(type: "text", nullable: true),
                    UserBasket = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: true),
                    UsedCuponCodeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OurServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "OurServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SubscriptionStep = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => new { x.UserId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_UserClaims_OperationClaims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserIp = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePathOrBase64 = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<string>(type: "text", nullable: true),
                    DocumentType = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Base64Data = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkType = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "tr-TR", "Türkçe" },
                    { 2, "en-US", "English" }
                });

            migrationBuilder.InsertData(
                table: "Translates",
                columns: new[] { "Id", "Code", "LangId", "LanguagesId", "Value" },
                values: new object[,]
                {
                    { 1, "Login", 1, null, "Giriş" },
                    { 2, "Email", 1, null, "E posta" },
                    { 3, "Password", 1, null, "Parola" },
                    { 4, "Update", 1, null, "Güncelle" },
                    { 5, "Delete", 1, null, "Sil" },
                    { 6, "UsersGroups", 1, null, "Kullanıcının Grupları" },
                    { 7, "UsersClaims", 1, null, "Kullanıcının Yetkileri" },
                    { 8, "Create", 1, null, "Yeni" },
                    { 9, "Users", 1, null, "Kullanıcılar" },
                    { 10, "Groups", 1, null, "Gruplar" },
                    { 11, "Login", 2, null, "Login" },
                    { 12, "Email", 2, null, "Email" },
                    { 13, "Password", 2, null, "Password" },
                    { 14, "Update", 2, null, "Update" },
                    { 15, "Delete", 2, null, "Delete" },
                    { 16, "UsersGroups", 2, null, "User's Groups" },
                    { 17, "UsersClaims", 2, null, "User's Claims" },
                    { 18, "Create", 2, null, "Create" },
                    { 19, "Users", 2, null, "Users" },
                    { 20, "Groups", 2, null, "Groups" },
                    { 21, "OperationClaim", 1, null, "Operasyon Yetkileri" },
                    { 22, "OperationClaim", 2, null, "Operation Claim" },
                    { 23, "Languages", 1, null, "Diller" },
                    { 24, "Languages", 2, null, "Languages" },
                    { 25, "TranslateWords", 1, null, "Dil Çevirileri" },
                    { 26, "TranslateWords", 2, null, "Translate Words" },
                    { 27, "Management", 1, null, "Yönetim" },
                    { 28, "Management", 2, null, "Management" },
                    { 29, "AppMenu", 1, null, "Uygulama" },
                    { 30, "AppMenu", 2, null, "Application" },
                    { 31, "Added", 1, null, "Başarıyla Eklendi." },
                    { 32, "Added", 2, null, "Successfully Added." },
                    { 33, "Updated", 1, null, "Başarıyla Güncellendi." },
                    { 34, "Updated", 2, null, "Successfully Updated." },
                    { 35, "Deleted", 1, null, "Başarıyla Silindi." },
                    { 36, "Deleted", 2, null, "Successfully Deleted." },
                    { 41, "CouldNotBeVerifyCid", 1, null, "Kimlik No Doğrulanamadı." },
                    { 43, "VerifyCid", 1, null, "Kimlik No Doğrulandı." },
                    { 44, "VerifyCid", 2, null, "Verify Citizen Id" },
                    { 51, "SuccessfulLogin", 1, null, "Sisteme giriş başarılı." },
                    { 59, "CID", 1, null, "Vatandaşlık No" },
                    { 60, "CID", 2, null, "Citizenship Number" },
                    { 61, "PasswordEmpty", 1, null, "Parola boş olamaz!" },
                    { 62, "PasswordEmpty", 2, null, "Password can not be empty!" },
                    { 69, "PasswordDigit", 1, null, "En Az 1 Rakam İçermelidir!" },
                    { 75, "InvalidCode", 1, null, "Geçersiz Bir Kod Girdiniz!" },
                    { 76, "InvalidCode", 2, null, "You Entered An Invalid Code!" },
                    { 83, "Unknown", 1, null, "Bilinmiyor!" },
                    { 84, "Unknown", 2, null, "Unknown!" },
                    { 85, "NewPassword", 1, null, "Yeni Parola:" },
                    { 86, "NewPassword", 2, null, "New Password:" },
                    { 87, "ChangePassword", 1, null, "Parola Değiştir" },
                    { 88, "ChangePassword", 2, null, "Change Password" },
                    { 89, "Save", 1, null, "Kaydet" },
                    { 90, "Save", 2, null, "Save" },
                    { 91, "GroupName", 1, null, "Grup Adı" },
                    { 92, "GroupName", 2, null, "Group Name" },
                    { 93, "FullName", 1, null, "Tam Adı" },
                    { 94, "FullName", 2, null, "Full Name" },
                    { 95, "Address", 1, null, "Adres" },
                    { 96, "Address", 2, null, "Address" },
                    { 97, "Notes", 1, null, "Notlar" },
                    { 98, "Notes", 2, null, "Notes" },
                    { 99, "ConfirmPassword", 1, null, "Parolayı Doğrula" },
                    { 100, "ConfirmPassword", 2, null, "Confirm Password" },
                    { 101, "Code", 1, null, "Kod" },
                    { 102, "Code", 2, null, "Code" },
                    { 103, "Alias", 1, null, "Görünen Ad" },
                    { 104, "Alias", 2, null, "Alias" },
                    { 105, "Description", 1, null, "Açıklama" },
                    { 106, "Description", 2, null, "Description" },
                    { 107, "Value", 1, null, "Değer" },
                    { 108, "Value", 2, null, "Value" },
                    { 109, "LangCode", 1, null, "Dil Kodu" },
                    { 110, "LangCode", 2, null, "Lang Code" },
                    { 111, "Name", 1, null, "Adı" },
                    { 112, "Name", 2, null, "Name" },
                    { 113, "MobilePhones", 1, null, "Cep Telefonu" },
                    { 114, "MobilePhones", 2, null, "Mobile Phone" },
                    { 115, "NoRecordsFound", 1, null, "Kayıt Bulunamadı" },
                    { 116, "NoRecordsFound", 2, null, "No Records Found" },
                    { 117, "Required", 1, null, "Bu alan zorunludur!" },
                    { 118, "Required", 2, null, "This field is required!" },
                    { 119, "Permissions", 1, null, "Permissions" },
                    { 120, "Permissions", 2, null, "İzinler" },
                    { 121, "GroupList", 1, null, "Grup Listesi" },
                    { 122, "GroupList", 2, null, "Group List" },
                    { 123, "GrupPermissions", 1, null, "Grup Yetkileri" },
                    { 124, "GrupPermissions", 2, null, "Grup Permissions" },
                    { 125, "Add", 1, null, "Ekle" },
                    { 126, "Add", 2, null, "Add" },
                    { 127, "UserList", 1, null, "Kullanıcı Listesi" },
                    { 128, "UserList", 2, null, "User List" },
                    { 129, "OperationClaimList", 1, null, "Yetki Listesi" },
                    { 130, "OperationClaimList", 2, null, "OperationClaim List" },
                    { 131, "LanguageList", 1, null, "Dil Listesi" },
                    { 132, "LanguageList", 2, null, "Language List" },
                    { 133, "TranslateList", 1, null, "Dil Çeviri Listesi" },
                    { 134, "TranslateList", 2, null, "Translate List" },
                    { 135, "LogList", 1, null, "İşlem Kütüğü" },
                    { 136, "LogList", 2, null, "LogList" },
                    { 137, "DeleteConfirm", 1, null, "Emin misiniz?" },
                    { 138, "DeleteConfirm", 2, null, "Are you sure?" },
                    { 139, "AppName", 1, null, "Flutter DevArchitecture" },
                    { 140, "AppName", 2, null, "Flutter DevArchitecture" },
                    { 177, "OperationClaimExists", 1, null, "Bu operasyon izni zaten mevcut." },
                    { 178, "OperationClaimExists", 2, null, "This operation permit already exists." },
                    { 179, "StringLengthMustBeGreaterThanThree", 1, null, "Lütfen En Az 3 Karakterden Oluşan Bir İfade Girin." },
                    { 180, "StringLengthMustBeGreaterThanThree", 2, null, "Please Enter A Phrase Of At Least 3 Characters." },
                    { 182, "CouldNotBeVerifyCid", 2, null, "Could not be verify Citizen Id" },
                    { 185, "AuthorizationsDenied", 1, null, "Yetkiniz olmayan bir alana girmeye çalıştığınız tespit edildi." },
                    { 186, "AuthorizationsDenied", 2, null, "It has been detected that you are trying to enter an area that you do not have authorization." },
                    { 187, "UserNotFound", 1, null, "Kimlik Bilgileri Doğrulanamadı. Lütfen Yeni Kayıt Ekranını kullanın." },
                    { 188, "UserNotFound", 2, null, "Credentials Could Not Verify. Please use the New Registration Screen." },
                    { 189, "PasswordError", 1, null, "Kimlik Bilgileri Doğrulanamadı, Kullanıcı adı ve/veya parola hatalı." },
                    { 190, "PasswordError", 2, null, "Credentials Failed to Authenticate, Username and / or password incorrect." },
                    { 192, "SuccessfulLogin", 2, null, "Login to the system is successful." },
                    { 193, "SendMobileCode", 1, null, "Lütfen Size SMS Olarak Gönderilen Kodu Girin!" },
                    { 194, "SendMobileCode", 2, null, "Please Enter The Code Sent To You By SMS!" },
                    { 195, "NameAlreadyExist", 1, null, "Oluşturmaya Çalıştığınız Nesne Zaten Var." },
                    { 196, "NameAlreadyExist", 2, null, "The Object You Are Trying To Create Already Exists." },
                    { 197, "WrongCitizenId", 1, null, "Vatandaşlık No Sistemimizde Bulunamadı. Lütfen Yeni Kayıt Oluşturun!" },
                    { 198, "WrongCitizenId", 2, null, "Citizenship Number Not Found In Our System. Please Create New Registration!" },
                    { 199, "CitizenNumber", 1, null, "Vatandaşlık No" },
                    { 200, "CitizenNumber", 2, null, "Citizenship Number" },
                    { 203, "PasswordLength", 1, null, "Minimum 8 Karakter Uzunluğunda Olmalıdır!" },
                    { 204, "PasswordLength", 2, null, "Must be at least 8 characters long!" },
                    { 205, "PasswordUppercaseLetter", 1, null, "En Az 1 Büyük Harf İçermelidir!" },
                    { 206, "PasswordUppercaseLetter", 2, null, "Must Contain At Least 1 Capital Letter!" },
                    { 207, "PasswordLowercaseLetter", 1, null, "En Az 1 Küçük Harf İçermelidir!" },
                    { 208, "PasswordLowercaseLetter", 2, null, "Must Contain At Least 1 Lowercase Letter!" },
                    { 210, "PasswordDigit", 2, null, "It Must Contain At Least 1 Digit!" },
                    { 211, "PasswordSpecialCharacter", 1, null, "En Az 1 Simge İçermelidir!" },
                    { 212, "PasswordSpecialCharacter", 2, null, "Must Contain At Least 1 Symbol!" },
                    { 213, "SendPassword", 1, null, "Yeni Parolanız E-Posta Adresinize Gönderildi." },
                    { 214, "SendPassword", 2, null, "Your new password has been sent to your e-mail address." },
                    { 217, "SmsServiceNotFound", 1, null, "SMS Servisine Ulaşılamıyor." },
                    { 218, "SmsServiceNotFound", 2, null, "Unable to Reach SMS Service." },
                    { 219, "TrueButCellPhone", 1, null, "Bilgiler doğru. Cep telefonu gerekiyor." },
                    { 220, "TrueButCellPhone", 2, null, "The information is correct. Cell phone is required." },
                    { 221, "TokenProviderException", 1, null, "Token Provider boş olamaz!" },
                    { 222, "TokenProviderException", 2, null, "Token Provider cannot be empty!" },
                    { 277, "CantBeEmpty", 1, null, "boş bırakılamaz!" },
                    { 278, "CantBeEmpty", 2, null, "can not be empty!" },
                    { 279, "InvalidEmail", 1, null, "Lütfen geçerli bir e-posta giriniz!" },
                    { 280, "InvalidEmail", 2, null, "Please enter a valid email address!" },
                    { 281, "InvalidPassword", 1, null, "Lütfen geçerli bir parola giriniz!" },
                    { 282, "InvalidPassword", 2, null, "Please enter a valid password address!" },
                    { 283, "Status", 1, null, "Durum" },
                    { 284, "Status", 2, null, "Status" },
                    { 285, "UpdateUser", 1, null, "Kullanıcı bilgilerini düzenle" },
                    { 286, "UpdateUser", 2, null, "Update User" },
                    { 287, "PageNotFound", 1, null, "Sayfa bulunamadı!" },
                    { 288, "PageNotFound", 2, null, "Page Not Found!" },
                    { 289, "Loading", 1, null, "Gerekli işlemler yapılıyor" },
                    { 290, "Loading", 2, null, "Loading" },
                    { 291, "HomePage", 1, null, "Anasayfa" },
                    { 292, "HomePage", 2, null, "Home Page" },
                    { 293, "ReturnHomePage", 1, null, "Anasayfaya Geri Dön" },
                    { 294, "ReturnHomePage", 2, null, "Return Home Page" },
                    { 295, "Categories", 1, null, "Kategoriler" },
                    { 296, "Categories", 2, null, "Categories" },
                    { 297, "Sells", 1, null, "Satışlar" },
                    { 298, "Sells", 2, null, "Sells" },
                    { 299, "Days", 1, null, "Günler" },
                    { 300, "Days", 2, null, "Days" },
                    { 301, "Sales", 1, null, "Satışlar" },
                    { 302, "Sales", 2, null, "Sales" },
                    { 305, "Months", 1, null, "Aylar" },
                    { 306, "Months", 2, null, "Months" },
                    { 307, "Years", 1, null, "Yıllar" },
                    { 308, "Years", 2, null, "Years" },
                    { 309, "Hours", 1, null, "Saatler" },
                    { 310, "Hours", 2, null, "Hours" },
                    { 311, "Minutes", 1, null, "Dakikalar" },
                    { 312, "Minutes", 2, null, "Minutes" },
                    { 313, "Seconds", 1, null, "Saniyeler" },
                    { 314, "Seconds", 2, null, "Seconds" },
                    { 315, "Today", 1, null, "Bugün" },
                    { 316, "Today", 2, null, "Today" },
                    { 317, "Yesterday", 1, null, "Dün" },
                    { 318, "Yesterday", 2, null, "Yesterday" },
                    { 319, "Week", 1, null, "Hafta" },
                    { 320, "Week", 2, null, "Week" },
                    { 321, "Month", 1, null, "Ay" },
                    { 322, "Month", 2, null, "Month" },
                    { 323, "Year", 1, null, "Yıl" },
                    { 324, "Year", 2, null, "Year" },
                    { 325, "Day", 1, null, "Gün" },
                    { 326, "Day", 2, null, "Day" },
                    { 327, "Hour", 1, null, "Saat" },
                    { 328, "Hour", 2, null, "Hour" },
                    { 329, "Minute", 1, null, "Dakika" },
                    { 330, "Minute", 2, null, "Minute" },
                    { 331, "Second", 1, null, "Saniye" },
                    { 332, "Second", 2, null, "Second" },
                    { 333, "DataTableTitle", 1, null, "Veri Tablosu" },
                    { 334, "DataTableTitle", 2, null, "Data Table Title" },
                    { 335, "PdfDownloadTooltip", 1, null, "PDF İndir" },
                    { 336, "PdfDownloadTooltip", 2, null, "Download PDF" },
                    { 337, "ExcelDownloadTooltip", 1, null, "Excel İndir" },
                    { 338, "ExcelDownloadTooltip", 2, null, "Download Excel" },
                    { 339, "CsvDownloadTooltip", 1, null, "CSV İndir" },
                    { 340, "CsvDownloadTooltip", 2, null, "Download CSV" },
                    { 341, "ImageDownloadTooltip", 1, null, "Resim İndir" },
                    { 342, "ImageDownloadTooltip", 2, null, "Download Image" },
                    { 343, "TxtDownloadTooltip", 1, null, "TXT İndir" },
                    { 344, "TxtDownloadTooltip", 2, null, "Download TXT" },
                    { 345, "JsonDownloadTooltip", 1, null, "JSON İndir" },
                    { 346, "JsonDownloadTooltip", 2, null, "Download JSON" },
                    { 347, "XmlDownloadTooltip", 1, null, "XML İndir" },
                    { 348, "XmlDownloadTooltip", 2, null, "Download XML" },
                    { 349, "PdfShareTooltip", 1, null, "PDF Paylaş" },
                    { 350, "PdfShareTooltip", 2, null, "Share PDF" },
                    { 351, "ExcelShareTooltip", 1, null, "Excel Paylaş" },
                    { 352, "ExcelShareTooltip", 2, null, "Share Excel" },
                    { 353, "CsvShareTooltip", 1, null, "CSV Paylaş" },
                    { 354, "CsvShareTooltip", 2, null, "Share CSV" },
                    { 355, "ImageShareTooltip", 1, null, "Resim Paylaş" },
                    { 356, "ImageShareTooltip", 2, null, "Share Image" },
                    { 357, "TxtShareTooltip", 1, null, "TXT Paylaş" },
                    { 358, "TxtShareTooltip", 2, null, "Share TXT" },
                    { 359, "JsonShareTooltip", 1, null, "JSON Paylaş" },
                    { 360, "JsonShareTooltip", 2, null, "Share JSON" },
                    { 361, "XmlShareTooltip", 1, null, "XML Paylaş" },
                    { 362, "XmlShareTooltip", 2, null, "Share XML" },
                    { 363, "ShareTitle", 1, null, "Paylaşım" },
                    { 364, "ShareTitle", 2, null, "Share Title" },
                    { 365, "NoData", 1, null, "Veri Yok" },
                    { 366, "NoData", 2, null, "No Data" },
                    { 367, "NoConnection", 1, null, "Bağlantı Yok" },
                    { 368, "NoConnection", 2, null, "No Connection" },
                    { 369, "NoDataFound", 1, null, "Veri Bulunamadı" },
                    { 370, "NoDataFound", 2, null, "No Data Found" },
                    { 371, "Error", 1, null, "Hata" },
                    { 372, "Error", 2, null, "Error" },
                    { 373, "Attention", 1, null, "Dikkat" },
                    { 374, "Attention", 2, null, "Attention" },
                    { 375, "Warning", 1, null, "Uyarı" },
                    { 376, "Warning", 2, null, "Warning" },
                    { 377, "Success", 1, null, "Başarılı" },
                    { 378, "Success", 2, null, "Success" },
                    { 379, "Information", 1, null, "Bilgi" },
                    { 380, "Information", 2, null, "Information" },
                    { 381, "Ok", 1, null, "Tamam" },
                    { 382, "Ok", 2, null, "Ok" },
                    { 383, "Cancel", 1, null, "İptal" },
                    { 384, "Cancel", 2, null, "Cancel" },
                    { 385, "Yes", 1, null, "Evet" },
                    { 386, "Yes", 2, null, "Yes" },
                    { 387, "No", 1, null, "Hayır" },
                    { 388, "No", 2, null, "No" },
                    { 393, "ThisActionCannotBeUndone", 1, null, "Bu işlem geri alınamaz" },
                    { 394, "ThisActionCannotBeUndone", 2, null, "This action cannot be undone" },
                    { 395, "NoDataAvailable", 1, null, "Mevcut veri yok" },
                    { 396, "NoDataAvailable", 2, null, "No Data Available" },
                    { 397, "VehicleRegistrationNumber", 1, null, "Araç Kayıt Numarası" },
                    { 398, "VehicleRegistrationNumber", 2, null, "Vehicle Registration Number" },
                    { 399, "VehicleLicensePlate", 1, null, "Araç Plakası" },
                    { 400, "VehicleLicensePlate", 2, null, "Vehicle License Plate" },
                    { 401, "PhoneNumber", 1, null, "Telefon Numarası" },
                    { 402, "PhoneNumber", 2, null, "Phone Number" },
                    { 403, "Search", 1, null, "Ara" },
                    { 404, "Search", 2, null, "Search" },
                    { 405, "AdminPanel", 1, null, "Yönetim Paneli" },
                    { 406, "AdminPanel", 2, null, "Admin Panel" },
                    { 407, "GroupInfoHover", 1, null, "Gruplar burada listelenmektedir." },
                    { 408, "GroupInfoHover", 2, null, "The group is listed." },
                    { 409, "AtLeastOneSelection", 1, null, "En Az Bir Seçim Yapılmalıdır" },
                    { 410, "AtLeastOneSelection", 2, null, "At Least One Selection Is Required" },
                    { 411, "LanguageInfoHover", 1, null, "Diller burada listelenmektedir." },
                    { 412, "LanguageInfoHover", 2, null, "The languages are listed." },
                    { 413, "LogInfoHover", 1, null, "Log bilgileri burada listelenmektedir." },
                    { 414, "LogInfoHover", 2, null, "Log information is listed." },
                    { 415, "OperationClaimInfoHover", 1, null, "Operasyon yetkileri burada listelenmekteidr." },
                    { 416, "OperationClaimInfoHover", 2, null, "Operation claims are listed." },
                    { 417, "TranslateInfoHover", 1, null, "Dil çevirileri burada listelenmektedir." },
                    { 418, "TranslateInfoHover", 2, null, "The language translations are listed." },
                    { 419, "PasswordsDoNotMatch", 1, null, "Şifreler eşleşmiyor" },
                    { 420, "PasswordsDoNotMatch", 2, null, "Passwords do not match" },
                    { 421, "GroupClaims", 1, null, "Grup Talepleri" },
                    { 422, "GroupClaims", 2, null, "Group Claims" },
                    { 423, "SelectGroupClaim", 1, null, "Grup Talebi Seçin" },
                    { 424, "SelectGroupClaim", 2, null, "Select Group Claim" },
                    { 429, "GroupNameHint", 1, null, "Grup Adı İpucu" },
                    { 430, "GroupNameHint", 2, null, "Group Name Hint" },
                    { 431, "AddGroup", 1, null, "Grup Ekle" },
                    { 432, "AddGroup", 2, null, "Add Group" },
                    { 433, "UpdateGroupClaims", 1, null, "Grup Taleplerini Güncelle" },
                    { 434, "UpdateGroupClaims", 2, null, "Update Group Claims" },
                    { 435, "UpdateButton", 1, null, "Güncelle" },
                    { 436, "UpdateButton", 2, null, "Update" },
                    { 437, "UpdateGroup", 1, null, "Grubu Güncelle" },
                    { 438, "UpdateGroup", 2, null, "Update Group" },
                    { 439, "UpdateGroupUsers", 1, null, "Grup Kullanıcılarını Güncelle" },
                    { 440, "UpdateGroupUsers", 2, null, "Update Group Users" },
                    { 445, "AddLanguage", 1, null, "Dil Ekle" },
                    { 446, "AddLanguage", 2, null, "Add Language" },
                    { 447, "UpdateLanguage", 1, null, "Dili Güncelle" },
                    { 448, "UpdateLanguage", 2, null, "Update Language" },
                    { 451, "Level", 1, null, "Seviye" },
                    { 452, "Level", 2, null, "Level" },
                    { 453, "ExceptionMessage", 1, null, "İstisna Mesajı" },
                    { 454, "ExceptionMessage", 2, null, "Exception Message" },
                    { 455, "TimeStamp", 1, null, "Zaman Damgası" },
                    { 456, "TimeStamp", 2, null, "Time Stamp" },
                    { 457, "User", 1, null, "Kullanıcı" },
                    { 458, "User", 2, null, "User" },
                    { 461, "Type", 1, null, "Tür" },
                    { 462, "Type", 2, null, "Type" },
                    { 463, "UpdateOperationClaim", 1, null, "Operasyon Talebini Güncelle" },
                    { 464, "UpdateOperationClaim", 2, null, "Update Operation Claim" },
                    { 465, "AliasHint", 1, null, "Takma Ad İpucu" },
                    { 466, "AliasHint", 2, null, "Alias Hint" },
                    { 621, "Language", 1, null, "Dil" },
                    { 622, "Language", 2, null, "Language" },
                    { 625, "CodeHint", 1, null, "Kod İpucu" },
                    { 626, "CodeHint", 2, null, "Code Hint" },
                    { 627, "ValueHint", 1, null, "Değer İpucu" },
                    { 628, "ValueHint", 2, null, "Value Hint" },
                    { 629, "AddTranslate", 1, null, "Çeviri Ekle" },
                    { 630, "AddTranslate", 2, null, "Add Translate" },
                    { 631, "UpdateTranslate", 1, null, "Çeviriyi Güncelle" },
                    { 632, "UpdateTranslate", 2, null, "Update Translate" },
                    { 633, "UserClaims", 1, null, "Kullanıcı Talepleri" },
                    { 634, "UserClaims", 2, null, "User Claims" },
                    { 635, "SelectUserClaims", 1, null, "Kullanıcı Taleplerini Seçin" },
                    { 636, "SelectUserClaims", 2, null, "Select User Claims" },
                    { 637, "SelectUsers", 1, null, "Kullanıcıları Seçin" },
                    { 638, "SelectUsers", 2, null, "Select Users" },
                    { 641, "UserGroups", 1, null, "Kullanıcı Grupları" },
                    { 642, "UserGroups", 2, null, "User Groups" },
                    { 643, "SelectUserGroups", 1, null, "Kullanıcı Gruplarını Seçin" },
                    { 644, "SelectUserGroups", 2, null, "Select User Groups" },
                    { 645, "AddUser", 1, null, "Kullanıcı Ekle" },
                    { 646, "AddUser", 2, null, "Add User" },
                    { 649, "FullNameHint", 1, null, "Tam Ad İpucu" },
                    { 650, "FullNameHint", 2, null, "Full Name Hint" },
                    { 653, "AddressHint", 1, null, "Adres İpucu" },
                    { 654, "AddressHint", 2, null, "Address Hint" },
                    { 655, "Active", 1, null, "Aktif" },
                    { 656, "Active", 2, null, "Active" },
                    { 657, "Inactive", 1, null, "Pasif" },
                    { 658, "Inactive", 2, null, "Inactive" },
                    { 661, "NotesHint", 1, null, "Notlar İpucu" },
                    { 662, "NotesHint", 2, null, "Notes Hint" },
                    { 669, "UpdateUserClaims", 1, null, "Kullanıcı Taleplerini Güncelle" },
                    { 670, "UpdateUserClaims", 2, null, "Update User Claims" },
                    { 671, "UpdateUserGroups", 1, null, "Kullanıcı Gruplarını Güncelle" },
                    { 672, "UpdateUserGroups", 2, null, "Update User Groups" },
                    { 673, "UpdateUsers", 1, null, "Kullanıcıları Güncelle" },
                    { 674, "UpdateUsers", 2, null, "Update Users" },
                    { 687, "Edit", 1, null, "Düzenle" },
                    { 688, "Edit", 2, null, "Edit" },
                    { 697, "OK", 1, null, "Tamam" },
                    { 698, "OK", 2, null, "OK" },
                    { 699, "Back", 1, null, "Geri" },
                    { 700, "Back", 2, null, "Back" },
                    { 701, "Next", 1, null, "İleri" },
                    { 702, "Next", 2, null, "Next" },
                    { 707, "Filter", 1, null, "Filtrele" },
                    { 708, "Filter", 2, null, "Filter" },
                    { 709, "Clear", 1, null, "Temizle" },
                    { 710, "Clear", 2, null, "Clear" },
                    { 711, "Sort", 1, null, "Sırala" },
                    { 712, "Sort", 2, null, "Sort" },
                    { 713, "Ascending", 1, null, "Artan" },
                    { 714, "Ascending", 2, null, "Ascending" },
                    { 715, "Descending", 1, null, "Azalan" },
                    { 716, "Descending", 2, null, "Descending" },
                    { 719, "ReturnHome", 1, null, "Anasayfaya dön" },
                    { 720, "ReturnHome", 2, null, "Return Home" },
                    { 721, "Welcome", 1, null, "Hoşgeldiniz" },
                    { 722, "Welcome", 2, null, "Welcome" },
                    { 723, "LogIn", 1, null, "Giriş Yap" },
                    { 724, "LogIn", 2, null, "Log In" },
                    { 725, "LogOut", 1, null, "Çıkış Yap" },
                    { 726, "LogOut", 2, null, "Log Out" },
                    { 727, "Register", 1, null, "Kayıt Ol" },
                    { 728, "Register", 2, null, "Register" },
                    { 735, "Username", 1, null, "Kullanıcı Adı" },
                    { 736, "Username", 2, null, "Username" },
                    { 737, "ForgotPassword", 1, null, "Şifremi Unuttum" },
                    { 738, "ForgotPassword", 2, null, "Forgot Password" },
                    { 739, "RememberMe", 1, null, "Beni Hatırla" },
                    { 740, "RememberMe", 2, null, "Remember Me" },
                    { 741, "Submit", 1, null, "Gönder" },
                    { 742, "Submit", 2, null, "Submit" },
                    { 743, "Reset", 1, null, "Sıfırla" },
                    { 744, "Reset", 2, null, "Reset" },
                    { 745, "Retry", 1, null, "Yeniden Dene" },
                    { 746, "Retry", 2, null, "Retry" },
                    { 755, "Confirm", 1, null, "Onayla" },
                    { 756, "Confirm", 2, null, "Confirm" },
                    { 757, "Exit", 1, null, "Çıkış" },
                    { 758, "Exit", 2, null, "Exit" },
                    { 759, "Close", 1, null, "Kapat" },
                    { 760, "Close", 2, null, "Close" },
                    { 761, "Open", 1, null, "Aç" },
                    { 762, "Open", 2, null, "Open" },
                    { 763, "Settings", 1, null, "Ayarlar" },
                    { 764, "Settings", 2, null, "Settings" },
                    { 765, "Help", 1, null, "Yardım" },
                    { 766, "Help", 2, null, "Help" },
                    { 767, "About", 1, null, "Hakkında" },
                    { 768, "About", 2, null, "About" },
                    { 769, "SubscriptionPageTitle", 1, null, "Abonelik Yönetimi" },
                    { 770, "SubscriptionPageTitle", 2, null, "Subscription Management" },
                    { 771, "AdminPanelPageInvoiceTitle", 1, null, "Faturalar" },
                    { 772, "AdminPanelPageInvoiceTitle", 2, null, "Invoices" },
                    { 773, "AdminPanelPageContractTitle", 1, null, "Sözleşmeler" },
                    { 774, "AdminPanelPageContractTitle", 2, null, "Contracts" },
                    { 775, "AdminPanelPageOrderTitle", 1, null, "Siparişler" },
                    { 776, "AdminPanelPageOrderTitle", 2, null, "Orders" },
                    { 777, "AdminPanelPageCompaniesDocTitle", 1, null, "Şirket Dökümanları" },
                    { 778, "AdminPanelPageCompaniesDocTitle", 2, null, "Companies Docs" },
                    { 779, "AdminPanelOurServicesTitle", 1, null, "Hizmetlerimiz" },
                    { 780, "AdminPanelOurServicesTitle", 2, null, "Our Services" },
                    { 781, "AdminPanelCuponCodesTitle", 1, null, "Kupon Kodları" },
                    { 782, "AdminPanelCuponCodesTitle", 2, null, "Cupon Codes" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CuponCodes_UserId",
                table: "CuponCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_UserId",
                table: "Evaluations",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaims_OperationClaimId",
                table: "GroupClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileLogins_ExternalUserId_Provider",
                table: "MobileLogins",
                columns: new[] { "ExternalUserId", "Provider" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Translates_LanguagesId",
                table: "Translates",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_ClaimId",
                table: "UserClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserId",
                table: "UserDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MobilePhones",
                table: "Users",
                column: "MobilePhones",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWorks_UserId",
                table: "UserWorks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "CompaniesDocs");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "CuponCodes");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "GroupClaims");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MobileLogins");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Translates");

            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserDocuments");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "UserWorks");

            migrationBuilder.DropTable(
                name: "OurServices");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
