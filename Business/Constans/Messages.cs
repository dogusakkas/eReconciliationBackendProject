using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    public class Messages
    {
        public static string AddedCompany = "Şirket kaydı başarı ile tamamlandı";
        public static string CompanyAlreadyExists = "Bu şirket daha önce kaydedilmiş";

        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifreniz Yanlış";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserRegistered = "Kayıt Başarılı";
        public static string UserAllReadyExists = "Bu kullanıcı zaten var";
        public static string UserMailConfirmSuccessful = "Mailiniz başarıyla onaylandı.";
        public static string MailAlreadyConfirm = "Mailiniz zaten onaylı.";
        public static string MailConfirmTimeHasNotExpired = "Mail onayınızı 3 dakikada 1 defa gönderebilirsiniz ";
       
        public static string MailParameterUpdated= "Mail parametreleri başarıyla güncellendi";
        public static string MailSendSuccessful = "Mail başarıyla gönderildi";
        public static string MailConfirmSendSuccessful = "Onay maili tekrar gönderildi";

        public static string MailTemplateAdded = "Mail şablonu başarıyla kaydedildi";
        public static string MailTemplateUpdated = "Mail şablonu başarıyla güncellendi";
        public static string MailTemplateDeleted = "Mail şablonu başarıyla silindi";
    }
}
