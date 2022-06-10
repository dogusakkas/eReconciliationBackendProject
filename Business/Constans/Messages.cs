using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    public class Messages
    {
        // Company
        public static string AddedCompany = "Şirket kaydı başarı ile tamamlandı";
        public static string UpdateCompany = "Şirket kaydı başarı ile güncellendi";
        public static string CompanyAlreadyExists = "Bu şirket daha önce kaydedilmiş";

        // User
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifreniz Yanlış";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserRegistered = "Kayıt Başarılı";
        public static string UserAllReadyExists = "Bu kullanıcı zaten var";
        public static string UserMailConfirmSuccessful = "Mailiniz başarıyla onaylandı.";
        public static string MailAlreadyConfirm = "Mailiniz zaten onaylı.";
        public static string MailConfirmTimeHasNotExpired = "Mail onayınızı 3 dakikada 1 defa gönderebilirsiniz ";

        // Mail Parameter
        public static string MailParameterUpdated = "Mail parametreleri başarıyla güncellendi";
        public static string MailSendSuccessful = "Mail başarıyla gönderildi";
        public static string MailConfirmSendSuccessful = "Onay maili tekrar gönderildi";

        // Mail Template
        public static string MailTemplateAdded = "Mail şablonu başarıyla kaydedildi";
        public static string MailTemplateUpdated = "Mail şablonu başarıyla güncellendi";
        public static string MailTemplateDeleted = "Mail şablonu başarıyla silindi";

        // Currency Account
        public static string AddedCurrencyAccount = "Cari kaydı başarıyla eklendi";
        public static string DeletedCurrencyAccount = "Cari kaydı başarıyla silindi";
        public static string UpdatedCurrencyAccount = "Cari kaydı başarıyla güncellendi";

        // Account Reconciliation
        public static string AddedAccountReconciliation = "Cari mütakabat kaydı başarılıyla eklendi";
        public static string UpdatedAccountReconciliation = "Cari mütakabat kaydı başarıyla güncellendi";
        public static string DeletedAccountReconciliation = "Cari mütakabat kaydı başarılıyla silindi";

        // Account Reconciliation Detail
        public static string AddedAccountReconciliationDetail = "Cari mütakabat detay kaydı başarılıyla eklendi";
        public static string UpdatedAccountReconciliationDetail = "Cari mütakabat detay kaydı başarılıyla güncellendi";
        public static string DeletedAccountReconciliationDetail = "Cari mütakabat detay kaydı başarılıyla silindi";

        // Ba Bs Reconciliation
        public static string AddedBaBsReconciliation = "BaBs kaydı başarıyla eklendi";
        public static string UpdatedBaBsReconciliation = "BaBs kaydı başarıyla güncellendi";
        public static string DeletedBaBsReconciliation = "BaBs kaydı başarıyla silindi";

        // Ba Bs Reconciliation Detail
        public static string AddedBaBsReconciliationDetail = "BaBs detay kaydı başarıyla eklendi";
        public static string UpdatedBaBsReconciliationDetail = "BaBs detay kaydı başarıyla güncellendi";
        public static string DeletedBaBsReconciliationDetail = "BaBs detay kaydı başarıyla silindi";

        // Operation Claim
        public static string AddedOperationClaim = "Yetki başarıyla eklendi";
        public static string UpdatedOperationClaim = "Yetki başarıyla güncellendi";
        public static string DeletedOperationClaim = "Yetki başarıyla silindi";

        // User Operation Claim
        public static string AddedUserOperationClaim = "Kullanıcıya yetki başarıyla eklendi";
        public static string UpdatedUserOperationClaim = "Kullanıcıya yetki başarıyla güncellendi";
        public static string DeletedUserOperationClaim = "Kullanıcıya yetki başarıyla silindi";


    }
}
