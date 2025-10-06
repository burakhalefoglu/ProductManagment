using System;
using System.Threading.Tasks;
using Business.Fakes.Handlers.Languages;
using Business.Fakes.Handlers.Translates;
using Business.Handlers.Groups.Commands;
using Core.Utilities.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helpers
{
    public static class FakeDataMiddlleware
    {
        public static async Task UseDbFakeDataCreator(this IApplicationBuilder app)
        {
            var mediator = ServiceTool.ServiceProvider.GetService<IMediator>();

            await mediator.Send(new CreateLanguageInternalCommand { Code = "tr-TR", Name = "Türkçe" });
            await mediator.Send(new CreateLanguageInternalCommand { Code = "en-EN", Name = "English" });


            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Welcome", Value = "Hoş geldiniz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ProductManagement", Value = "Ürün Yönetimi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "CloseSidebar", Value = "Menüyü Kapat" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ADMIN", Value = "YÖNETİM" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "APP", Value = "UYGULAMA" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Profile", Value = "Profil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Logout", Value = "Çıkış" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OpenSidebar", Value = "Menüyü Aç" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Logo", Value = "Logo" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Welcome", Value = "Welcome" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ProductManagement", Value = "Product Management" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "CloseSidebar", Value = "Close Sidebar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ADMIN", Value = "ADMIN" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "APP", Value = "APP" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Profile", Value = "Profile" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Logout", Value = "Logout" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OpenSidebar", Value = "Open Sidebar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Logo", Value = "Logo" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Login", Value = "Giriş" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ProductManagement", Value = "Ürün Yönetimi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Email", Value = "E-posta" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseEnterValidEmail", Value = "Lütfen geçerli bir e-posta adresi girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Password", Value = "Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseEnterYourPassword", Value = "Lütfen şifrenizi girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectLanguage", Value = "Dil Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseSelect", Value = "Lütfen Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DontHaveAnAccountRegister", Value = "Hesabınız yok mu? Kayıt olun" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Login", Value = "Login" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ProductManagement", Value = "Product Management" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Email", Value = "Email" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseEnterValidEmail", Value = "Please enter a valid email address" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Password", Value = "Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseEnterYourPassword", Value = "Please enter your password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectLanguage", Value = "Select Language" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseSelect", Value = "Please Select" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DontHaveAnAccountRegister", Value = "Don't have an account? Register" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Register", Value = "Kayıt Ol" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ProductManagement", Value = "Ürün Yönetimi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Username", Value = "Kullanıcı Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseEnterUsername", Value = "Lütfen kullanıcı adınızı girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Email", Value = "E-posta" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseEnterValidEmail", Value = "Lütfen geçerli bir e-posta adresi girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Password", Value = "Şifre" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseEnterPassword", Value = "Lütfen şifrenizi girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ConfirmPassword", Value = "Şifreyi Onayla" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PasswordsDoNotMatchOrEmpty", Value = "Şifreler uyuşmuyor veya boş bıraktınız" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "SelectLanguage", Value = "Dil Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PleaseSelect", Value = "Lütfen Seçin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "AlreadyHaveAnAccountLogin", Value = "Zaten hesabınız var mı? Giriş yapın" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Register", Value = "Register" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ProductManagement", Value = "Product Management" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Username", Value = "Username" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseEnterUsername", Value = "Please enter your username" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Email", Value = "Email" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseEnterValidEmail", Value = "Please enter a valid email address" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Password", Value = "Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseEnterPassword", Value = "Please enter your password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ConfirmPassword", Value = "Confirm Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PasswordsDoNotMatchOrEmpty", Value = "Passwords do not match or are empty" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "SelectLanguage", Value = "Select Language" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PleaseSelect", Value = "Please Select" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "AlreadyHaveAnAccountLogin", Value = "Already have an account? Login" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.ADD.TITLE", Value = "Yeni Ürün Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.NAME", Value = "Ürün Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.PRICE", Value = "Fiyat" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.STOCK", Value = "Stok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.COLORS", Value = "Renkler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.COLOR_NAME", Value = "Renk Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.ADD_COLOR", Value = "Renk Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "VALIDATION.NAME_REQUIRED", Value = "Ürün adı zorunludur" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "VALIDATION.PRICE_INVALID", Value = "Lütfen geçerli bir fiyat girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "VALIDATION.STOCK_INVALID", Value = "Lütfen geçerli bir stok miktarı girin" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.SAVE", Value = "Kaydet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.CANCEL", Value = "İptal" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.ADD.TITLE", Value = "Add New Product" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.NAME", Value = "Product Name" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.PRICE", Value = "Price" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.STOCK", Value = "Stock" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.COLORS", Value = "Colors" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.COLOR_NAME", Value = "Color Name" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.ADD_COLOR", Value = "Add Color" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "VALIDATION.NAME_REQUIRED", Value = "Product name is required" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "VALIDATION.PRICE_INVALID", Value = "Please enter a valid price" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "VALIDATION.STOCK_INVALID", Value = "Please enter a valid stock quantity" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.SAVE", Value = "Save" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.CANCEL", Value = "Cancel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.DETAIL.TITLE", Value = "Ürün Detayı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.LOADING", Value = "Yükleniyor" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.ID", Value = "Ürün ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.COLOR", Value = "Renk" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.NO_COLORS", Value = "Tanımlı renk yok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.UPDATE", Value = "Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.BACK_TO_LIST", Value = "Listeye Dön" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.DETAIL.TITLE", Value = "Product Detail" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.LOADING", Value = "Loading" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.ID", Value = "Product ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.COLOR", Value = "Color" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.NO_COLORS", Value = "No colors defined" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.UPDATE", Value = "Update" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.BACK_TO_LIST", Value = "Back to List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.LIST.TITLE", Value = "Ürün Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.RELOAD", Value = "Yenile" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.ADD_NEW", Value = "Yeni Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.SEARCH_PLACEHOLDER", Value = "Ara..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.NO_DATA_AVAILABLE", Value = "Gösterilecek veri yok" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.LIST.EXPORT_FILENAME", Value = "urunler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ID", Value = "ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.ACTIONS", Value = "İşlemler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.LIST.TOTAL", Value = "Toplam: {0}" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GENERAL.DETAIL", Value = "Detay" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.LIST.TITLE", Value = "Product List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.RELOAD", Value = "Reload" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.ADD_NEW", Value = "Add New" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.SEARCH_PLACEHOLDER", Value = "Search..." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.NO_DATA_AVAILABLE", Value = "No data available" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.LIST.EXPORT_FILENAME", Value = "products" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ID", Value = "ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.ACTIONS", Value = "Actions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.LIST.TOTAL", Value = "Total: {0}" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GENERAL.DETAIL", Value = "Detail" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "PRODUCT.UPDATE.TITLE", Value = "Ürünü Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "PRODUCT.UPDATE.TITLE", Value = "Update Product" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DASHBOARD.SUMMARY", Value = "Özet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DASHBOARD.TITLE", Value = "Kontrol Paneli" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DASHBOARD.WELCOME_TITLE", Value = "Hoş geldiniz" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DASHBOARD.SUMMARY_TEXT", Value = "Sistemin genel durumunu buradan görüntüleyebilirsiniz." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DASHBOARD.SUMMARY", Value = "Summary" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DASHBOARD.TITLE", Value = "Dashboard" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DASHBOARD.WELCOME_TITLE", Value = "Welcome" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DASHBOARD.SUMMARY_TEXT", Value = "You can view the overall system status here." });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupList", Value = "Grup Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Add", Value = "Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Filter", Value = "Filtrele" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Id", Value = "ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupName", Value = "Grup Adı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Actions", Value = "İşlemler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GrupPermissions", Value = "Grup Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UsersGroups", Value = "Kullanıcı Grupları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Update", Value = "Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Delete", Value = "Sil" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupForm", Value = "Grup Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Required", Value = "Zorunlu alan" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Save", Value = "Kaydet" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupUsers", Value = "Grup Kullanıcıları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Users", Value = "Kullanıcılar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "GroupClaims", Value = "Grup Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Claims", Value = "Yetkiler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "DeleteConfirm", Value = "Bu grubu silmek istediğinize emin misiniz?" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Cancel", Value = "İptal" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OK", Value = "Tamam" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupList", Value = "Group List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Add", Value = "Add" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Filter", Value = "Filter" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Id", Value = "ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupName", Value = "Group Name" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Actions", Value = "Actions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GrupPermissions", Value = "Group Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UsersGroups", Value = "User Groups" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Update", Value = "Update" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Delete", Value = "Delete" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupForm", Value = "Group Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Required", Value = "Required field" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Save", Value = "Save" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupUsers", Value = "Group Users" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Users", Value = "Users" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "GroupClaims", Value = "Group Claims" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Claims", Value = "Claims" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "DeleteConfirm", Value = "Are you sure you want to delete this group?" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Cancel", Value = "Cancel" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OK", Value = "OK" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LanguageList", Value = "Dil Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Id", Value = "ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Name", Value = "Ad" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Code", Value = "Kod" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LanguageForm", Value = "Dil Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "FullName", Value = "Tam Ad" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LanguageList", Value = "Language List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Id", Value = "ID" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Name", Value = "Name" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Code", Value = "Code" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LanguageForm", Value = "Language Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "FullName", Value = "Full Name" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LogList", Value = "Log Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Level", Value = "Seviye" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ExceptionMessage", Value = "Hata Mesajı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TimeStamp", Value = "Zaman Damgası" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "User", Value = "Kullanıcı" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Value", Value = "Değer" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Type", Value = "Tür" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LogList", Value = "Log List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Level", Value = "Level" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ExceptionMessage", Value = "Exception Message" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TimeStamp", Value = "Timestamp" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "User", Value = "User" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Value", Value = "Value" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Type", Value = "Type" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OperationClaimList", Value = "Yetki Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Alias", Value = "Kısa Ad" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Description", Value = "Açıklama" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "OperationClaimForm", Value = "Yetki Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OperationClaimList", Value = "Operation Claim List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Alias", Value = "Alias" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Description", Value = "Description" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OperationClaimForm", Value = "Operation Claim Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TranslateList", Value = "Çeviri Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LangCode", Value = "Dil Kodu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TranslateForm", Value = "Çeviri Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TranslateList", Value = "Translation List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LangCode", Value = "Language Code" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TranslateForm", Value = "Translation Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserList", Value = "Kullanıcı Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserForm", Value = "Kullanıcı Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ChangePassword", Value = "Şifre Değiştir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UsersClaims", Value = "Kullanıcı Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Permissions", Value = "Yetkiler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Groups", Value = "Gruplar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Status", Value = "Durum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "MobilePhones", Value = "Cep Telefonları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Address", Value = "Adres" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Notes", Value = "Notlar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserList", Value = "User List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserForm", Value = "User Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ChangePassword", Value = "Change Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UsersClaims", Value = "Users' Claims" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Permissions", Value = "Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Groups", Value = "Groups" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Status", Value = "Status" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "MobilePhones", Value = "Mobile Phones" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Address", Value = "Address" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Notes", Value = "Notes" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OperationClaimList", Value = "Operation Claim List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Alias", Value = "Alias" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Description", Value = "Description" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "OperationClaimForm", Value = "Operation Claim Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TranslateList", Value = "Çeviri Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "LangCode", Value = "Dil Kodu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "TranslateForm", Value = "Çeviri Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TranslateList", Value = "Translation List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "LangCode", Value = "Language Code" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "TranslateForm", Value = "Translation Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserList", Value = "Kullanıcı Listesi" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UserForm", Value = "Kullanıcı Formu" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "ChangePassword", Value = "Şifre Değiştir" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "UsersClaims", Value = "Kullanıcı Yetkileri" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Permissions", Value = "Yetkiler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Groups", Value = "Gruplar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Status", Value = "Durum" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "MobilePhones", Value = "Cep Telefonları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Address", Value = "Adres" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Notes", Value = "Notlar" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserList", Value = "User List" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UserForm", Value = "User Form" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "ChangePassword", Value = "Change Password" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "UsersClaims", Value = "Users' Claims" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Permissions", Value = "Permissions" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Groups", Value = "Groups" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Status", Value = "Status" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "MobilePhones", Value = "Mobile Phones" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Address", Value = "Address" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Notes", Value = "Notes" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Products", Value = "Ürünler" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Products", Value = "Products" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Product Add", Value = "Ürün Ekle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Product Add", Value = "Product Add" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Product Detail", Value = "Ürün Ayrıntıları" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Product Detail", Value = "Product Details" });

            await mediator.Send(new CreateTranslateInternalCommand { LangId = 1, Code = "Product Detail", Value = "Ürün Güncelle" });
            await mediator.Send(new CreateTranslateInternalCommand { LangId = 2, Code = "Product Detail", Value = "Product Update" });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 1, 
                Code = "Operation Claim", 
                Value = "Yetki" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 2, 
                Code = "Operation Claim", 
                Value = "Operation Claim" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 1, 
                Code = "Languages", 
                Value = "Diller" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 2, 
                Code = "Languages", 
                Value = "Languages" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 1, 
                Code = "Translations", 
                Value = "Çeviriler" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 2, 
                Code = "Translations", 
                Value = "Translations" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 1, 
                Code = "Logs", 
                Value = "İzleme Kayıtları" 
            });

            await mediator.Send(new CreateTranslateInternalCommand 
            { 
                LangId = 2, 
                Code = "Logs", 
                Value = "Logs" 
            });

            // Create default group
            await mediator.Send(new CreateGroupInternalCommand
            {
                GroupName = "Default Group"
            });
        }
    }
}