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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitsInStock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
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
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColorName = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colors_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
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
                    { 1, "Welcome", 1, null, "Hoş geldiniz" },
                    { 2, "ProductManagement", 1, null, "Ürün Yönetimi" },
                    { 3, "CloseSidebar", 1, null, "Menüyü Kapat" },
                    { 4, "ADMIN", 1, null, "YÖNETİM" },
                    { 5, "APP", 1, null, "UYGULAMA" },
                    { 6, "Profile", 1, null, "Profil" },
                    { 7, "Logout", 1, null, "Çıkış" },
                    { 8, "OpenSidebar", 1, null, "Menüyü Aç" },
                    { 9, "Logo", 1, null, "Logo" },
                    { 10, "Welcome", 2, null, "Welcome" },
                    { 11, "ProductManagement", 2, null, "Product Management" },
                    { 12, "CloseSidebar", 2, null, "Close Sidebar" },
                    { 13, "ADMIN", 2, null, "ADMIN" },
                    { 14, "APP", 2, null, "APP" },
                    { 15, "Profile", 2, null, "Profile" },
                    { 16, "Logout", 2, null, "Logout" },
                    { 17, "OpenSidebar", 2, null, "Open Sidebar" },
                    { 18, "Logo", 2, null, "Logo" },
                    { 19, "Login", 1, null, "Giriş" },
                    { 20, "ProductManagement", 1, null, "Ürün Yönetimi" },
                    { 21, "Email", 1, null, "E-posta" },
                    { 22, "PleaseEnterValidEmail", 1, null, "Lütfen geçerli bir e-posta adresi girin" },
                    { 23, "Password", 1, null, "Şifre" },
                    { 24, "PleaseEnterYourPassword", 1, null, "Lütfen şifrenizi girin" },
                    { 25, "SelectLanguage", 1, null, "Dil Seçin" },
                    { 26, "PleaseSelect", 1, null, "Lütfen Seçin" },
                    { 27, "DontHaveAnAccountRegister", 1, null, "Hesabınız yok mu? Kayıt olun" },
                    { 28, "Login", 2, null, "Login" },
                    { 29, "ProductManagement", 2, null, "Product Management" },
                    { 30, "Email", 2, null, "Email" },
                    { 31, "PleaseEnterValidEmail", 2, null, "Please enter a valid email address" },
                    { 32, "Password", 2, null, "Password" },
                    { 33, "PleaseEnterYourPassword", 2, null, "Please enter your password" },
                    { 34, "SelectLanguage", 2, null, "Select Language" },
                    { 35, "PleaseSelect", 2, null, "Please Select" },
                    { 36, "DontHaveAnAccountRegister", 2, null, "Don't have an account? Register" },
                    { 37, "Register", 1, null, "Kayıt Ol" },
                    { 38, "ProductManagement", 1, null, "Ürün Yönetimi" },
                    { 39, "Username", 1, null, "Kullanıcı Adı" },
                    { 40, "PleaseEnterUsername", 1, null, "Lütfen kullanıcı adınızı girin" },
                    { 41, "Email", 1, null, "E-posta" },
                    { 42, "PleaseEnterValidEmail", 1, null, "Lütfen geçerli bir e-posta adresi girin" },
                    { 43, "Password", 1, null, "Şifre" },
                    { 44, "PleaseEnterPassword", 1, null, "Lütfen şifrenizi girin" },
                    { 45, "ConfirmPassword", 1, null, "Şifreyi Onayla" },
                    { 46, "PasswordsDoNotMatchOrEmpty", 1, null, "Şifreler uyuşmuyor veya boş bıraktınız" },
                    { 47, "SelectLanguage", 1, null, "Dil Seçin" },
                    { 48, "PleaseSelect", 1, null, "Lütfen Seçin" },
                    { 49, "AlreadyHaveAnAccountLogin", 1, null, "Zaten hesabınız var mı? Giriş yapın" },
                    { 50, "Register", 2, null, "Register" },
                    { 51, "ProductManagement", 2, null, "Product Management" },
                    { 52, "Username", 2, null, "Username" },
                    { 53, "PleaseEnterUsername", 2, null, "Please enter your username" },
                    { 54, "Email", 2, null, "Email" },
                    { 55, "PleaseEnterValidEmail", 2, null, "Please enter a valid email address" },
                    { 56, "Password", 2, null, "Password" },
                    { 57, "PleaseEnterPassword", 2, null, "Please enter your password" },
                    { 58, "ConfirmPassword", 2, null, "Confirm Password" },
                    { 59, "PasswordsDoNotMatchOrEmpty", 2, null, "Passwords do not match or are empty" },
                    { 60, "SelectLanguage", 2, null, "Select Language" },
                    { 61, "PleaseSelect", 2, null, "Please Select" },
                    { 62, "AlreadyHaveAnAccountLogin", 2, null, "Already have an account? Login" },
                    { 63, "PRODUCT.ADD.TITLE", 1, null, "Yeni Ürün Ekle" },
                    { 64, "PRODUCT.NAME", 1, null, "Ürün Adı" },
                    { 65, "PRODUCT.PRICE", 1, null, "Fiyat" },
                    { 66, "PRODUCT.STOCK", 1, null, "Stok" },
                    { 67, "PRODUCT.COLORS", 1, null, "Renkler" },
                    { 68, "PRODUCT.COLOR_NAME", 1, null, "Renk Adı" },
                    { 69, "PRODUCT.ADD_COLOR", 1, null, "Renk Ekle" },
                    { 70, "VALIDATION.NAME_REQUIRED", 1, null, "Ürün adı zorunludur" },
                    { 71, "VALIDATION.PRICE_INVALID", 1, null, "Lütfen geçerli bir fiyat girin" },
                    { 72, "VALIDATION.STOCK_INVALID", 1, null, "Lütfen geçerli bir stok miktarı girin" },
                    { 73, "GENERAL.SAVE", 1, null, "Kaydet" },
                    { 74, "GENERAL.CANCEL", 1, null, "İptal" },
                    { 75, "PRODUCT.ADD.TITLE", 2, null, "Add New Product" },
                    { 76, "PRODUCT.NAME", 2, null, "Product Name" },
                    { 77, "PRODUCT.PRICE", 2, null, "Price" },
                    { 78, "PRODUCT.STOCK", 2, null, "Stock" },
                    { 79, "PRODUCT.COLORS", 2, null, "Colors" },
                    { 80, "PRODUCT.COLOR_NAME", 2, null, "Color Name" },
                    { 81, "PRODUCT.ADD_COLOR", 2, null, "Add Color" },
                    { 82, "VALIDATION.NAME_REQUIRED", 2, null, "Product name is required" },
                    { 83, "VALIDATION.PRICE_INVALID", 2, null, "Please enter a valid price" },
                    { 84, "VALIDATION.STOCK_INVALID", 2, null, "Please enter a valid stock quantity" },
                    { 85, "GENERAL.SAVE", 2, null, "Save" },
                    { 86, "GENERAL.CANCEL", 2, null, "Cancel" },
                    { 87, "PRODUCT.DETAIL.TITLE", 1, null, "Ürün Detayı" },
                    { 88, "GENERAL.LOADING", 1, null, "Yükleniyor" },
                    { 89, "PRODUCT.ID", 1, null, "Ürün ID" },
                    { 90, "PRODUCT.COLOR", 1, null, "Renk" },
                    { 91, "PRODUCT.NO_COLORS", 1, null, "Tanımlı renk yok" },
                    { 92, "GENERAL.UPDATE", 1, null, "Güncelle" },
                    { 93, "GENERAL.BACK_TO_LIST", 1, null, "Listeye Dön" },
                    { 94, "PRODUCT.DETAIL.TITLE", 2, null, "Product Detail" },
                    { 95, "GENERAL.LOADING", 2, null, "Loading" },
                    { 96, "PRODUCT.ID", 2, null, "Product ID" },
                    { 97, "PRODUCT.COLOR", 2, null, "Color" },
                    { 98, "PRODUCT.NO_COLORS", 2, null, "No colors defined" },
                    { 99, "GENERAL.UPDATE", 2, null, "Update" },
                    { 100, "GENERAL.BACK_TO_LIST", 2, null, "Back to List" },
                    { 101, "PRODUCT.LIST.TITLE", 1, null, "Ürün Listesi" },
                    { 102, "GENERAL.RELOAD", 1, null, "Yenile" },
                    { 103, "GENERAL.ADD_NEW", 1, null, "Yeni Ekle" },
                    { 104, "GENERAL.SEARCH_PLACEHOLDER", 1, null, "Ara..." },
                    { 105, "GENERAL.NO_DATA_AVAILABLE", 1, null, "Gösterilecek veri yok" },
                    { 106, "PRODUCT.LIST.EXPORT_FILENAME", 1, null, "urunler" },
                    { 107, "ID", 1, null, "ID" },
                    { 108, "GENERAL.ACTIONS", 1, null, "İşlemler" },
                    { 109, "PRODUCT.LIST.TOTAL", 1, null, "Toplam: {0}" },
                    { 110, "GENERAL.DETAIL", 1, null, "Detay" },
                    { 111, "PRODUCT.LIST.TITLE", 2, null, "Product List" },
                    { 112, "GENERAL.RELOAD", 2, null, "Reload" },
                    { 113, "GENERAL.ADD_NEW", 2, null, "Add New" },
                    { 114, "GENERAL.SEARCH_PLACEHOLDER", 2, null, "Search..." },
                    { 115, "GENERAL.NO_DATA_AVAILABLE", 2, null, "No data available" },
                    { 116, "PRODUCT.LIST.EXPORT_FILENAME", 2, null, "products" },
                    { 117, "ID", 2, null, "ID" },
                    { 118, "GENERAL.ACTIONS", 2, null, "Actions" },
                    { 119, "PRODUCT.LIST.TOTAL", 2, null, "Total: {0}" },
                    { 120, "GENERAL.DETAIL", 2, null, "Detail" },
                    { 121, "PRODUCT.UPDATE.TITLE", 1, null, "Ürünü Güncelle" },
                    { 122, "PRODUCT.UPDATE.TITLE", 2, null, "Update Product" },
                    { 123, "DASHBOARD.SUMMARY", 1, null, "Özet" },
                    { 124, "DASHBOARD.TITLE", 1, null, "Kontrol Paneli" },
                    { 125, "DASHBOARD.WELCOME_TITLE", 1, null, "Hoş geldiniz" },
                    { 126, "DASHBOARD.SUMMARY_TEXT", 1, null, "Sistemin genel durumunu buradan görüntüleyebilirsiniz." },
                    { 127, "DASHBOARD.SUMMARY", 2, null, "Summary" },
                    { 128, "DASHBOARD.TITLE", 2, null, "Dashboard" },
                    { 129, "DASHBOARD.WELCOME_TITLE", 2, null, "Welcome" },
                    { 130, "DASHBOARD.SUMMARY_TEXT", 2, null, "You can view the overall system status here." },
                    { 131, "GroupList", 1, null, "Grup Listesi" },
                    { 132, "Add", 1, null, "Ekle" },
                    { 133, "Filter", 1, null, "Filtrele" },
                    { 134, "Id", 1, null, "ID" },
                    { 135, "GroupName", 1, null, "Grup Adı" },
                    { 136, "Actions", 1, null, "İşlemler" },
                    { 137, "GrupPermissions", 1, null, "Grup Yetkileri" },
                    { 138, "UsersGroups", 1, null, "Kullanıcı Grupları" },
                    { 139, "Update", 1, null, "Güncelle" },
                    { 140, "Delete", 1, null, "Sil" },
                    { 141, "GroupForm", 1, null, "Grup Formu" },
                    { 142, "Required", 1, null, "Zorunlu alan" },
                    { 143, "Save", 1, null, "Kaydet" },
                    { 144, "GroupUsers", 1, null, "Grup Kullanıcıları" },
                    { 145, "Users", 1, null, "Kullanıcılar" },
                    { 146, "GroupClaims", 1, null, "Grup Yetkileri" },
                    { 147, "Claims", 1, null, "Yetkiler" },
                    { 148, "DeleteConfirm", 1, null, "Bu grubu silmek istediğinize emin misiniz?" },
                    { 149, "Cancel", 1, null, "İptal" },
                    { 150, "OK", 1, null, "Tamam" },
                    { 151, "GroupList", 2, null, "Group List" },
                    { 152, "Add", 2, null, "Add" },
                    { 153, "Filter", 2, null, "Filter" },
                    { 154, "Id", 2, null, "ID" },
                    { 155, "GroupName", 2, null, "Group Name" },
                    { 156, "Actions", 2, null, "Actions" },
                    { 157, "GrupPermissions", 2, null, "Group Permissions" },
                    { 158, "UsersGroups", 2, null, "User Groups" },
                    { 159, "Update", 2, null, "Update" },
                    { 160, "Delete", 2, null, "Delete" },
                    { 161, "GroupForm", 2, null, "Group Form" },
                    { 162, "Required", 2, null, "Required field" },
                    { 163, "Save", 2, null, "Save" },
                    { 164, "GroupUsers", 2, null, "Group Users" },
                    { 165, "Users", 2, null, "Users" },
                    { 166, "GroupClaims", 2, null, "Group Claims" },
                    { 167, "Claims", 2, null, "Claims" },
                    { 168, "DeleteConfirm", 2, null, "Are you sure you want to delete this group?" },
                    { 169, "Cancel", 2, null, "Cancel" },
                    { 170, "OK", 2, null, "OK" },
                    { 171, "LanguageList", 1, null, "Dil Listesi" },
                    { 172, "Id", 1, null, "ID" },
                    { 173, "Name", 1, null, "Ad" },
                    { 174, "Code", 1, null, "Kod" },
                    { 175, "LanguageForm", 1, null, "Dil Formu" },
                    { 176, "FullName", 1, null, "Tam Ad" },
                    { 177, "LanguageList", 2, null, "Language List" },
                    { 178, "Id", 2, null, "ID" },
                    { 179, "Name", 2, null, "Name" },
                    { 180, "Code", 2, null, "Code" },
                    { 181, "LanguageForm", 2, null, "Language Form" },
                    { 182, "FullName", 2, null, "Full Name" },
                    { 183, "LogList", 1, null, "Log Listesi" },
                    { 184, "Level", 1, null, "Seviye" },
                    { 185, "ExceptionMessage", 1, null, "Hata Mesajı" },
                    { 186, "TimeStamp", 1, null, "Zaman Damgası" },
                    { 187, "User", 1, null, "Kullanıcı" },
                    { 188, "Value", 1, null, "Değer" },
                    { 189, "Type", 1, null, "Tür" },
                    { 190, "LogList", 2, null, "Log List" },
                    { 191, "Level", 2, null, "Level" },
                    { 192, "ExceptionMessage", 2, null, "Exception Message" },
                    { 193, "TimeStamp", 2, null, "Timestamp" },
                    { 194, "User", 2, null, "User" },
                    { 195, "Value", 2, null, "Value" },
                    { 196, "Type", 2, null, "Type" },
                    { 197, "OperationClaimList", 1, null, "Yetki Listesi" },
                    { 198, "Alias", 1, null, "Kısa Ad" },
                    { 199, "Description", 1, null, "Açıklama" },
                    { 200, "OperationClaimForm", 1, null, "Yetki Formu" },
                    { 201, "OperationClaimList", 2, null, "Operation Claim List" },
                    { 202, "Alias", 2, null, "Alias" },
                    { 203, "Description", 2, null, "Description" },
                    { 204, "OperationClaimForm", 2, null, "Operation Claim Form" },
                    { 205, "TranslateList", 1, null, "Çeviri Listesi" },
                    { 206, "LangCode", 1, null, "Dil Kodu" },
                    { 207, "TranslateForm", 1, null, "Çeviri Formu" },
                    { 208, "TranslateList", 2, null, "Translation List" },
                    { 209, "LangCode", 2, null, "Language Code" },
                    { 210, "TranslateForm", 2, null, "Translation Form" },
                    { 211, "UserList", 1, null, "Kullanıcı Listesi" },
                    { 212, "UserForm", 1, null, "Kullanıcı Formu" },
                    { 213, "ChangePassword", 1, null, "Şifre Değiştir" },
                    { 214, "UsersClaims", 1, null, "Kullanıcı Yetkileri" },
                    { 215, "Permissions", 1, null, "Yetkiler" },
                    { 216, "Groups", 1, null, "Gruplar" },
                    { 217, "Status", 1, null, "Durum" },
                    { 218, "MobilePhones", 1, null, "Cep Telefonları" },
                    { 219, "Address", 1, null, "Adres" },
                    { 220, "Notes", 1, null, "Notlar" },
                    { 221, "UserList", 2, null, "User List" },
                    { 222, "UserForm", 2, null, "User Form" },
                    { 223, "ChangePassword", 2, null, "Change Password" },
                    { 224, "UsersClaims", 2, null, "Users' Claims" },
                    { 225, "Permissions", 2, null, "Permissions" },
                    { 226, "Groups", 2, null, "Groups" },
                    { 227, "Status", 2, null, "Status" },
                    { 228, "MobilePhones", 2, null, "Mobile Phones" },
                    { 229, "Address", 2, null, "Address" },
                    { 230, "Notes", 2, null, "Notes" },
                    { 231, "Products", 1, null, "Ürünler" },
                    { 232, "Products", 2, null, "Products" },
                    { 233, "Product Add", 1, null, "Ürün Ekle" },
                    { 234, "Product Add", 2, null, "Product Add" },
                    { 235, "Product Detail", 1, null, "Ürün Ayrıntıları" },
                    { 236, "Product Detail", 2, null, "Product Details" },
                    { 237, "Product Detail", 1, null, "Ürün Güncelle" },
                    { 238, "Product Detail", 2, null, "Product Update" },
                    { 239, "Operation Claim", 1, null, "Yetki" },
                    { 240, "Operation Claim", 2, null, "Operation Claim" },
                    { 241, "Languages", 1, null, "Diller" },
                    { 242, "Languages", 2, null, "Languages" },
                    { 243, "Translations", 1, null, "Çeviriler" },
                    { 244, "Translations", 2, null, "Translations" },
                    { 245, "Logs", 1, null, "İzleme Kayıtları" },
                    { 246, "Logs", 2, null, "Logs" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ProductId",
                table: "Colors",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaims_OperationClaimId",
                table: "GroupClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileLogins_ExternalUserId_Provider",
                table: "MobileLogins",
                columns: new[] { "ExternalUserId", "Provider" });

            migrationBuilder.CreateIndex(
                name: "IX_Translates_LanguagesId",
                table: "Translates",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_ClaimId",
                table: "UserClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "GroupClaims");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MobileLogins");

            migrationBuilder.DropTable(
                name: "Translates");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Products");

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
