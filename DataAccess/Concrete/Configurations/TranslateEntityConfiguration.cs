using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DataAccess.Concrete.Configurations
{
    public class TranslateEntityConfiguration : BaseConfiguration<Translate>
    {
        public override void Configure(EntityTypeBuilder<Translate> builder)
        {
            builder.Property(x => x.LangId).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Value).HasMaxLength(500).IsRequired();
            builder.HasData(GetTranslates());

            base.Configure(builder);
        }

        private List<Translate> GetTranslates()
        {
            return new List<Translate>()
            {
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 1,  LangId = 1, Code = "Welcome",           Value = "Hoş geldiniz" },
                new Translate { Id = 2,  LangId = 1, Code = "ProductManagement", Value = "Ürün Yönetimi" },
                new Translate { Id = 3,  LangId = 1, Code = "CloseSidebar",      Value = "Menüyü Kapat" },
                new Translate { Id = 4,  LangId = 1, Code = "ADMIN",             Value = "YÖNETİM" },
                new Translate { Id = 5,  LangId = 1, Code = "APP",               Value = "UYGULAMA" },
                new Translate { Id = 6,  LangId = 1, Code = "Profile",           Value = "Profil" },
                new Translate { Id = 7,  LangId = 1, Code = "Logout",            Value = "Çıkış" },
                new Translate { Id = 8,  LangId = 1, Code = "OpenSidebar",       Value = "Menüyü Aç" },
                new Translate { Id = 9,  LangId = 1, Code = "Logo",              Value = "Logo" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 10, LangId = 2, Code = "Welcome",           Value = "Welcome" },
                new Translate { Id = 11, LangId = 2, Code = "ProductManagement", Value = "Product Management" },
                new Translate { Id = 12, LangId = 2, Code = "CloseSidebar",      Value = "Close Sidebar" },
                new Translate { Id = 13, LangId = 2, Code = "ADMIN",             Value = "ADMIN" },
                new Translate { Id = 14, LangId = 2, Code = "APP",               Value = "APP" },
                new Translate { Id = 15, LangId = 2, Code = "Profile",           Value = "Profile" },
                new Translate { Id = 16, LangId = 2, Code = "Logout",            Value = "Logout" },
                new Translate { Id = 17, LangId = 2, Code = "OpenSidebar",       Value = "Open Sidebar" },
                new Translate { Id = 18, LangId = 2, Code = "Logo",              Value = "Logo" },
                
                   // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 19, LangId = 1, Code = "Login",                     Value = "Giriş" },
                new Translate { Id = 20, LangId = 1, Code = "ProductManagement",         Value = "Ürün Yönetimi" },
                new Translate { Id = 21, LangId = 1, Code = "Email",                     Value = "E-posta" },
                new Translate { Id = 22, LangId = 1, Code = "PleaseEnterValidEmail",     Value = "Lütfen geçerli bir e-posta adresi girin" },
                new Translate { Id = 23, LangId = 1, Code = "Password",                  Value = "Şifre" },
                new Translate { Id = 24, LangId = 1, Code = "PleaseEnterYourPassword",   Value = "Lütfen şifrenizi girin" },
                new Translate { Id = 25, LangId = 1, Code = "SelectLanguage",            Value = "Dil Seçin" },
                new Translate { Id = 26, LangId = 1, Code = "PleaseSelect",              Value = "Lütfen Seçin" },
                new Translate { Id = 27, LangId = 1, Code = "DontHaveAnAccountRegister", Value = "Hesabınız yok mu? Kayıt olun" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 28, LangId = 2, Code = "Login",                     Value = "Login" },
                new Translate { Id = 29, LangId = 2, Code = "ProductManagement",         Value = "Product Management" },
                new Translate { Id = 30, LangId = 2, Code = "Email",                     Value = "Email" },
                new Translate { Id = 31, LangId = 2, Code = "PleaseEnterValidEmail",     Value = "Please enter a valid email address" },
                new Translate { Id = 32, LangId = 2, Code = "Password",                  Value = "Password" },
                new Translate { Id = 33, LangId = 2, Code = "PleaseEnterYourPassword",   Value = "Please enter your password" },
                new Translate { Id = 34, LangId = 2, Code = "SelectLanguage",            Value = "Select Language" },
                new Translate { Id = 35, LangId = 2, Code = "PleaseSelect",              Value = "Please Select" },
                new Translate { Id = 36, LangId = 2, Code = "DontHaveAnAccountRegister", Value = "Don't have an account? Register" },
                
                             // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 37, LangId = 1, Code = "Register",                    Value = "Kayıt Ol" },
                new Translate { Id = 38, LangId = 1, Code = "ProductManagement",           Value = "Ürün Yönetimi" },
                new Translate { Id = 39, LangId = 1, Code = "Username",                    Value = "Kullanıcı Adı" },
                new Translate { Id = 40, LangId = 1, Code = "PleaseEnterUsername",         Value = "Lütfen kullanıcı adınızı girin" },
                new Translate { Id = 41, LangId = 1, Code = "Email",                       Value = "E-posta" },
                new Translate { Id = 42, LangId = 1, Code = "PleaseEnterValidEmail",       Value = "Lütfen geçerli bir e-posta adresi girin" },
                new Translate { Id = 43, LangId = 1, Code = "Password",                    Value = "Şifre" },
                new Translate { Id = 44, LangId = 1, Code = "PleaseEnterPassword",         Value = "Lütfen şifrenizi girin" },
                new Translate { Id = 45, LangId = 1, Code = "ConfirmPassword",             Value = "Şifreyi Onayla" },
                new Translate { Id = 46, LangId = 1, Code = "PasswordsDoNotMatchOrEmpty",  Value = "Şifreler uyuşmuyor veya boş bıraktınız" },
                new Translate { Id = 47, LangId = 1, Code = "SelectLanguage",              Value = "Dil Seçin" },
                new Translate { Id = 48, LangId = 1, Code = "PleaseSelect",                Value = "Lütfen Seçin" },
                new Translate { Id = 49, LangId = 1, Code = "AlreadyHaveAnAccountLogin",   Value = "Zaten hesabınız var mı? Giriş yapın" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 50, LangId = 2, Code = "Register",                    Value = "Register" },
                new Translate { Id = 51, LangId = 2, Code = "ProductManagement",           Value = "Product Management" },
                new Translate { Id = 52, LangId = 2, Code = "Username",                    Value = "Username" },
                new Translate { Id = 53, LangId = 2, Code = "PleaseEnterUsername",         Value = "Please enter your username" },
                new Translate { Id = 54, LangId = 2, Code = "Email",                       Value = "Email" },
                new Translate { Id = 55, LangId = 2, Code = "PleaseEnterValidEmail",       Value = "Please enter a valid email address" },
                new Translate { Id = 56, LangId = 2, Code = "Password",                    Value = "Password" },
                new Translate { Id = 57, LangId = 2, Code = "PleaseEnterPassword",         Value = "Please enter your password" },
                new Translate { Id = 58, LangId = 2, Code = "ConfirmPassword",             Value = "Confirm Password" },
                new Translate { Id = 59, LangId = 2, Code = "PasswordsDoNotMatchOrEmpty",  Value = "Passwords do not match or are empty" },
                new Translate { Id = 60, LangId = 2, Code = "SelectLanguage",              Value = "Select Language" },
                new Translate { Id = 61, LangId = 2, Code = "PleaseSelect",                Value = "Please Select" },
                new Translate { Id = 62, LangId = 2, Code = "AlreadyHaveAnAccountLogin",   Value = "Already have an account? Login" },
                
                             // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 63, LangId = 1, Code = "PRODUCT.ADD.TITLE",       Value = "Yeni Ürün Ekle" },
                new Translate { Id = 64, LangId = 1, Code = "PRODUCT.NAME",            Value = "Ürün Adı" },
                new Translate { Id = 65, LangId = 1, Code = "PRODUCT.PRICE",           Value = "Fiyat" },
                new Translate { Id = 66, LangId = 1, Code = "PRODUCT.STOCK",           Value = "Stok" },
                new Translate { Id = 67, LangId = 1, Code = "PRODUCT.COLORS",          Value = "Renkler" },
                new Translate { Id = 68, LangId = 1, Code = "PRODUCT.COLOR_NAME",      Value = "Renk Adı" },
                new Translate { Id = 69, LangId = 1, Code = "PRODUCT.ADD_COLOR",       Value = "Renk Ekle" },
                new Translate { Id = 70, LangId = 1, Code = "VALIDATION.NAME_REQUIRED",Value = "Ürün adı zorunludur" },
                new Translate { Id = 71, LangId = 1, Code = "VALIDATION.PRICE_INVALID",Value = "Lütfen geçerli bir fiyat girin" },
                new Translate { Id = 72, LangId = 1, Code = "VALIDATION.STOCK_INVALID",Value = "Lütfen geçerli bir stok miktarı girin" },
                new Translate { Id = 73, LangId = 1, Code = "GENERAL.SAVE",            Value = "Kaydet" },
                new Translate { Id = 74, LangId = 1, Code = "GENERAL.CANCEL",          Value = "İptal" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 75, LangId = 2, Code = "PRODUCT.ADD.TITLE",       Value = "Add New Product" },
                new Translate { Id = 76, LangId = 2, Code = "PRODUCT.NAME",            Value = "Product Name" },
                new Translate { Id = 77, LangId = 2, Code = "PRODUCT.PRICE",           Value = "Price" },
                new Translate { Id = 78, LangId = 2, Code = "PRODUCT.STOCK",           Value = "Stock" },
                new Translate { Id = 79, LangId = 2, Code = "PRODUCT.COLORS",          Value = "Colors" },
                new Translate { Id = 80, LangId = 2, Code = "PRODUCT.COLOR_NAME",      Value = "Color Name" },
                new Translate { Id = 81, LangId = 2, Code = "PRODUCT.ADD_COLOR",       Value = "Add Color" },
                new Translate { Id = 82, LangId = 2, Code = "VALIDATION.NAME_REQUIRED",Value = "Product name is required" },
                new Translate { Id = 83, LangId = 2, Code = "VALIDATION.PRICE_INVALID",Value = "Please enter a valid price" },
                new Translate { Id = 84, LangId = 2, Code = "VALIDATION.STOCK_INVALID",Value = "Please enter a valid stock quantity" },
                new Translate { Id = 85, LangId = 2, Code = "GENERAL.SAVE",            Value = "Save" },
                new Translate { Id = 86, LangId = 2, Code = "GENERAL.CANCEL",          Value = "Cancel" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 87, LangId = 1, Code = "PRODUCT.DETAIL.TITLE",  Value = "Ürün Detayı" },
                new Translate { Id = 88, LangId = 1, Code = "GENERAL.LOADING",       Value = "Yükleniyor" },
                new Translate { Id = 89, LangId = 1, Code = "PRODUCT.ID",            Value = "Ürün ID" },
                new Translate { Id = 90, LangId = 1, Code = "PRODUCT.COLOR",         Value = "Renk" },
                new Translate { Id = 91, LangId = 1, Code = "PRODUCT.NO_COLORS",     Value = "Tanımlı renk yok" },
                new Translate { Id = 92, LangId = 1, Code = "GENERAL.UPDATE",        Value = "Güncelle" },
                new Translate { Id = 93, LangId = 1, Code = "GENERAL.BACK_TO_LIST",  Value = "Listeye Dön" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 94, LangId = 2, Code = "PRODUCT.DETAIL.TITLE",  Value = "Product Detail" },
                new Translate { Id = 95, LangId = 2, Code = "GENERAL.LOADING",       Value = "Loading" },
                new Translate { Id = 96, LangId = 2, Code = "PRODUCT.ID",            Value = "Product ID" },
                new Translate { Id = 97, LangId = 2, Code = "PRODUCT.COLOR",         Value = "Color" },
                new Translate { Id = 98, LangId = 2, Code = "PRODUCT.NO_COLORS",     Value = "No colors defined" },
                new Translate { Id = 99, LangId = 2, Code = "GENERAL.UPDATE",        Value = "Update" },
                new Translate { Id = 100, LangId = 2, Code = "GENERAL.BACK_TO_LIST", Value = "Back to List" },
                
                               // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 101, LangId = 1, Code = "PRODUCT.LIST.TITLE",        Value = "Ürün Listesi" },
                new Translate { Id = 102, LangId = 1, Code = "GENERAL.RELOAD",             Value = "Yenile" },
                new Translate { Id = 103, LangId = 1, Code = "GENERAL.ADD_NEW",            Value = "Yeni Ekle" },
                new Translate { Id = 104, LangId = 1, Code = "GENERAL.SEARCH_PLACEHOLDER", Value = "Ara..." },
                new Translate { Id = 105, LangId = 1, Code = "GENERAL.NO_DATA_AVAILABLE",  Value = "Gösterilecek veri yok" },
                new Translate { Id = 106, LangId = 1, Code = "PRODUCT.LIST.EXPORT_FILENAME", Value = "urunler" },
                new Translate { Id = 107, LangId = 1, Code = "ID",                         Value = "ID" },
                new Translate { Id = 108, LangId = 1, Code = "GENERAL.ACTIONS",            Value = "İşlemler" },
                new Translate { Id = 109, LangId = 1, Code = "PRODUCT.LIST.TOTAL",         Value = "Toplam: {0}" },
                new Translate { Id = 110, LangId = 1, Code = "GENERAL.DETAIL",             Value = "Detay" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 111, LangId = 2, Code = "PRODUCT.LIST.TITLE",        Value = "Product List" },
                new Translate { Id = 112, LangId = 2, Code = "GENERAL.RELOAD",             Value = "Reload" },
                new Translate { Id = 113, LangId = 2, Code = "GENERAL.ADD_NEW",            Value = "Add New" },
                new Translate { Id = 114, LangId = 2, Code = "GENERAL.SEARCH_PLACEHOLDER", Value = "Search..." },
                new Translate { Id = 115, LangId = 2, Code = "GENERAL.NO_DATA_AVAILABLE",  Value = "No data available" },
                new Translate { Id = 116, LangId = 2, Code = "PRODUCT.LIST.EXPORT_FILENAME", Value = "products" },
                new Translate { Id = 117, LangId = 2, Code = "ID",                         Value = "ID" },
                new Translate { Id = 118, LangId = 2, Code = "GENERAL.ACTIONS",            Value = "Actions" },
                new Translate { Id = 119, LangId = 2, Code = "PRODUCT.LIST.TOTAL",         Value = "Total: {0}" },
                new Translate { Id = 120, LangId = 2, Code = "GENERAL.DETAIL",             Value = "Detail" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 121, LangId = 1, Code = "PRODUCT.UPDATE.TITLE", Value = "Ürünü Güncelle" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 122, LangId = 2, Code = "PRODUCT.UPDATE.TITLE", Value = "Update Product" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 123, LangId = 1, Code = "DASHBOARD.SUMMARY",        Value = "Özet" },
                new Translate { Id = 124, LangId = 1, Code = "DASHBOARD.TITLE",          Value = "Kontrol Paneli" },
                new Translate { Id = 125, LangId = 1, Code = "DASHBOARD.WELCOME_TITLE",  Value = "Hoş geldiniz" },
                new Translate { Id = 126, LangId = 1, Code = "DASHBOARD.SUMMARY_TEXT",   Value = "Sistemin genel durumunu buradan görüntüleyebilirsiniz." },

                // ---- English (LangId = 2) ----
                new Translate { Id = 127, LangId = 2, Code = "DASHBOARD.SUMMARY",        Value = "Summary" },
                new Translate { Id = 128, LangId = 2, Code = "DASHBOARD.TITLE",          Value = "Dashboard" },
                new Translate { Id = 129, LangId = 2, Code = "DASHBOARD.WELCOME_TITLE",  Value = "Welcome" },
                new Translate { Id = 130, LangId = 2, Code = "DASHBOARD.SUMMARY_TEXT",   Value = "You can view the overall system status here." },
                
                             // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 131, LangId = 1, Code = "GroupList",           Value = "Grup Listesi" },
                new Translate { Id = 132, LangId = 1, Code = "Add",                 Value = "Ekle" },
                new Translate { Id = 133, LangId = 1, Code = "Filter",              Value = "Filtrele" },
                new Translate { Id = 134, LangId = 1, Code = "Id",                  Value = "ID" },
                new Translate { Id = 135, LangId = 1, Code = "GroupName",           Value = "Grup Adı" },
                new Translate { Id = 136, LangId = 1, Code = "Actions",             Value = "İşlemler" },
                new Translate { Id = 137, LangId = 1, Code = "GrupPermissions",     Value = "Grup Yetkileri" },
                new Translate { Id = 138, LangId = 1, Code = "UsersGroups",         Value = "Kullanıcı Grupları" },
                new Translate { Id = 139, LangId = 1, Code = "Update",              Value = "Güncelle" },
                new Translate { Id = 140, LangId = 1, Code = "Delete",              Value = "Sil" },
                new Translate { Id = 141, LangId = 1, Code = "GroupForm",           Value = "Grup Formu" },
                new Translate { Id = 142, LangId = 1, Code = "Required",            Value = "Zorunlu alan" },
                new Translate { Id = 143, LangId = 1, Code = "Save",                Value = "Kaydet" },
                new Translate { Id = 144, LangId = 1, Code = "GroupUsers",          Value = "Grup Kullanıcıları" },
                new Translate { Id = 145, LangId = 1, Code = "Users",               Value = "Kullanıcılar" },
                new Translate { Id = 146, LangId = 1, Code = "GroupClaims",         Value = "Grup Yetkileri" },
                new Translate { Id = 147, LangId = 1, Code = "Claims",              Value = "Yetkiler" },
                new Translate { Id = 148, LangId = 1, Code = "DeleteConfirm",       Value = "Bu grubu silmek istediğinize emin misiniz?" },
                new Translate { Id = 149, LangId = 1, Code = "Cancel",              Value = "İptal" },
                new Translate { Id = 150, LangId = 1, Code = "OK",                  Value = "Tamam" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 151, LangId = 2, Code = "GroupList",           Value = "Group List" },
                new Translate { Id = 152, LangId = 2, Code = "Add",                 Value = "Add" },
                new Translate { Id = 153, LangId = 2, Code = "Filter",              Value = "Filter" },
                new Translate { Id = 154, LangId = 2, Code = "Id",                  Value = "ID" },
                new Translate { Id = 155, LangId = 2, Code = "GroupName",           Value = "Group Name" },
                new Translate { Id = 156, LangId = 2, Code = "Actions",             Value = "Actions" },
                new Translate { Id = 157, LangId = 2, Code = "GrupPermissions",     Value = "Group Permissions" },
                new Translate { Id = 158, LangId = 2, Code = "UsersGroups",         Value = "User Groups" },
                new Translate { Id = 159, LangId = 2, Code = "Update",              Value = "Update" },
                new Translate { Id = 160, LangId = 2, Code = "Delete",              Value = "Delete" },
                new Translate { Id = 161, LangId = 2, Code = "GroupForm",           Value = "Group Form" },
                new Translate { Id = 162, LangId = 2, Code = "Required",            Value = "Required field" },
                new Translate { Id = 163, LangId = 2, Code = "Save",                Value = "Save" },
                new Translate { Id = 164, LangId = 2, Code = "GroupUsers",          Value = "Group Users" },
                new Translate { Id = 165, LangId = 2, Code = "Users",               Value = "Users" },
                new Translate { Id = 166, LangId = 2, Code = "GroupClaims",         Value = "Group Claims" },
                new Translate { Id = 167, LangId = 2, Code = "Claims",              Value = "Claims" },
                new Translate { Id = 168, LangId = 2, Code = "DeleteConfirm",       Value = "Are you sure you want to delete this group?" },
                new Translate { Id = 169, LangId = 2, Code = "Cancel",              Value = "Cancel" },
                new Translate { Id = 170, LangId = 2, Code = "OK",                  Value = "OK" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 171, LangId = 1, Code = "LanguageList", Value = "Dil Listesi" },
                new Translate { Id = 172, LangId = 1, Code = "Id",           Value = "ID" },
                new Translate { Id = 173, LangId = 1, Code = "Name",         Value = "Ad" },
                new Translate { Id = 174, LangId = 1, Code = "Code",         Value = "Kod" },
                new Translate { Id = 175, LangId = 1, Code = "LanguageForm", Value = "Dil Formu" },
                new Translate { Id = 176, LangId = 1, Code = "FullName",     Value = "Tam Ad" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 177, LangId = 2, Code = "LanguageList", Value = "Language List" },
                new Translate { Id = 178, LangId = 2, Code = "Id",           Value = "ID" },
                new Translate { Id = 179, LangId = 2, Code = "Name",         Value = "Name" },
                new Translate { Id = 180, LangId = 2, Code = "Code",         Value = "Code" },
                new Translate { Id = 181, LangId = 2, Code = "LanguageForm", Value = "Language Form" },
                new Translate { Id = 182, LangId = 2, Code = "FullName",     Value = "Full Name" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 183, LangId = 1, Code = "LogList",           Value = "Log Listesi" },
                new Translate { Id = 184, LangId = 1, Code = "Level",             Value = "Seviye" },
                new Translate { Id = 185, LangId = 1, Code = "ExceptionMessage",  Value = "Hata Mesajı" },
                new Translate { Id = 186, LangId = 1, Code = "TimeStamp",         Value = "Zaman Damgası" },
                new Translate { Id = 187, LangId = 1, Code = "User",              Value = "Kullanıcı" },
                new Translate { Id = 188, LangId = 1, Code = "Value",             Value = "Değer" },
                new Translate { Id = 189, LangId = 1, Code = "Type",              Value = "Tür" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 190, LangId = 2, Code = "LogList",           Value = "Log List" },
                new Translate { Id = 191, LangId = 2, Code = "Level",             Value = "Level" },
                new Translate { Id = 192, LangId = 2, Code = "ExceptionMessage",  Value = "Exception Message" },
                new Translate { Id = 193, LangId = 2, Code = "TimeStamp",         Value = "Timestamp" },
                new Translate { Id = 194, LangId = 2, Code = "User",              Value = "User" },
                new Translate { Id = 195, LangId = 2, Code = "Value",             Value = "Value" },
                new Translate { Id = 196, LangId = 2, Code = "Type",              Value = "Type" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 197, LangId = 1, Code = "OperationClaimList", Value = "Yetki Listesi" },
                new Translate { Id = 198, LangId = 1, Code = "Alias",              Value = "Kısa Ad" },
                new Translate { Id = 199, LangId = 1, Code = "Description",        Value = "Açıklama" },
                new Translate { Id = 200, LangId = 1, Code = "OperationClaimForm", Value = "Yetki Formu" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 201, LangId = 2, Code = "OperationClaimList", Value = "Operation Claim List" },
                new Translate { Id = 202, LangId = 2, Code = "Alias",              Value = "Alias" },
                new Translate { Id = 203, LangId = 2, Code = "Description",        Value = "Description" },
                new Translate { Id = 204, LangId = 2, Code = "OperationClaimForm", Value = "Operation Claim Form" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 205, LangId = 1, Code = "TranslateList", Value = "Çeviri Listesi" },
                new Translate { Id = 206, LangId = 1, Code = "LangCode",      Value = "Dil Kodu" },
                new Translate { Id = 207, LangId = 1, Code = "TranslateForm", Value = "Çeviri Formu" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 208, LangId = 2, Code = "TranslateList", Value = "Translation List" },
                new Translate { Id = 209, LangId = 2, Code = "LangCode",      Value = "Language Code" },
                new Translate { Id = 210, LangId = 2, Code = "TranslateForm", Value = "Translation Form" },
                
                // ---- Türkçe (LangId = 1) ----
                new Translate { Id = 211, LangId = 1, Code = "UserList",      Value = "Kullanıcı Listesi" },
                new Translate { Id = 212, LangId = 1, Code = "UserForm",      Value = "Kullanıcı Formu" },
                new Translate { Id = 213, LangId = 1, Code = "ChangePassword",Value = "Şifre Değiştir" },
                new Translate { Id = 214, LangId = 1, Code = "UsersClaims",   Value = "Kullanıcı Yetkileri" },
                new Translate { Id = 215, LangId = 1, Code = "Permissions",   Value = "Yetkiler" },
                new Translate { Id = 216, LangId = 1, Code = "Groups",        Value = "Gruplar" },
                new Translate { Id = 217, LangId = 1, Code = "Status",        Value = "Durum" },
                new Translate { Id = 218, LangId = 1, Code = "MobilePhones",  Value = "Cep Telefonları" },
                new Translate { Id = 219, LangId = 1, Code = "Address",       Value = "Adres" },
                new Translate { Id = 220, LangId = 1, Code = "Notes",         Value = "Notlar" },

                // ---- English (LangId = 2) ----
                new Translate { Id = 221, LangId = 2, Code = "UserList",      Value = "User List" },
                new Translate { Id = 222, LangId = 2, Code = "UserForm",      Value = "User Form" },
                new Translate { Id = 223, LangId = 2, Code = "ChangePassword",Value = "Change Password" },
                new Translate { Id = 224, LangId = 2, Code = "UsersClaims",   Value = "Users' Claims" },
                new Translate { Id = 225, LangId = 2, Code = "Permissions",   Value = "Permissions" },
                new Translate { Id = 226, LangId = 2, Code = "Groups",        Value = "Groups" },
                new Translate { Id = 227, LangId = 2, Code = "Status",        Value = "Status" },
                new Translate { Id = 228, LangId = 2, Code = "MobilePhones",  Value = "Mobile Phones" },
                new Translate { Id = 229, LangId = 2, Code = "Address",       Value = "Address" },
                new Translate { Id = 230, LangId = 2, Code = "Notes",         Value = "Notes" },

                new Translate { Id = 231, LangId = 1, Code = "Products",         Value = "Ürünler" },
                new Translate { Id = 232, LangId = 2, Code = "Products",         Value = "Products" },

                new Translate { Id = 233, LangId = 1, Code = "Product Add",         Value = "Ürün Ekle" },
                new Translate { Id = 234, LangId = 2, Code = "Product Add",         Value = "Product Add" },

                new Translate { Id = 235, LangId = 1, Code = "Product Detail",         Value = "Ürün Ayrıntıları" },
                new Translate { Id = 236, LangId = 2, Code = "Product Detail",         Value = "Product Details" },

                new Translate { Id = 237, LangId = 1, Code = "Product Detail",         Value = "Ürün Güncelle" },
                new Translate { Id = 238, LangId = 2, Code = "Product Detail",         Value = "Product Update" },

                new Translate { Id = 239, LangId = 1, Code = "Operation Claim", Value = "Yetki" },
                new Translate { Id = 240, LangId = 2, Code = "Operation Claim", Value = "Operation Claim" },

                new Translate { Id = 241, LangId = 1, Code = "Languages", Value = "Diller" },
                new Translate { Id = 242, LangId = 2, Code = "Languages", Value = "Languages" },

                new Translate { Id = 243, LangId = 1, Code = "Translations", Value = "Çeviriler" },
                new Translate { Id = 244, LangId = 2, Code = "Translations", Value = "Translations" },

                new Translate { Id = 245, LangId = 1, Code = "Logs", Value = "İzleme Kayıtları" },
                new Translate { Id = 246, LangId = 2, Code = "Logs", Value = "Logs" },

            };
        }
    }
}