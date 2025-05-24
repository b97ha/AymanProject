namespace AymanProject.Models
{
    public static class TranslationHelper
    {
        private static readonly Dictionary<string, Dictionary<string, string>> Translations = new()
        {
            // Navigation
            ["Home"] = new() { ["en"] = "Home", ["ar"] = "الرئيسية" },
            ["MainCriterians"] = new() { ["en"] = "Main Criterians", ["ar"] = "المعايير الرئيسية" },
            ["Projects"] = new() { ["en"] = "Projects", ["ar"] = "المشاريع" },
            ["SubCriterians"] = new() { ["en"] = "Sub Criterians", ["ar"] = "المعايير الفرعية" },

            // Common Actions
            ["Create"] = new() { ["en"] = "Create", ["ar"] = "إنشاء" },
            ["Edit"] = new() { ["en"] = "Edit", ["ar"] = "تعديل" },
            ["Delete"] = new() { ["en"] = "Delete", ["ar"] = "حذف" },
            ["Details"] = new() { ["en"] = "Details", ["ar"] = "التفاصيل" },
            ["Save"] = new() { ["en"] = "Save", ["ar"] = "حفظ" },
            ["Cancel"] = new() { ["en"] = "Cancel", ["ar"] = "إلغاء" },
            ["Back"] = new() { ["en"] = "Back", ["ar"] = "رجوع" },
            ["BackToList"] = new() { ["en"] = "Back to List", ["ar"] = "رجوع إلى القائمة" },
            ["Actions"] = new() { ["en"] = "Actions", ["ar"] = "الإجراءات" },
            ["List"] = new() { ["en"] = "List", ["ar"] = "القائمة" },

            // Project Fields
            ["Title"] = new() { ["en"] = "Title", ["ar"] = "العنوان" },
            ["Location"] = new() { ["en"] = "Location", ["ar"] = "الموقع" },
            ["Description"] = new() { ["en"] = "Description", ["ar"] = "الوصف" },
            ["SubmittedOn"] = new() { ["en"] = "Submitted On", ["ar"] = "تاريخ التقديم" },
            ["EndOn"] = new() { ["en"] = "End On", ["ar"] = "تاريخ الانتهاء" },

            // Criteria Fields
            ["TextArabic"] = new() { ["en"] = "Text (Arabic)", ["ar"] = "النص (عربي)" },
            ["TextEnglish"] = new() { ["en"] = "Text (English)", ["ar"] = "النص (إنجليزي)" },
            ["Weight"] = new() { ["en"] = "Weight", ["ar"] = "الوزن" },

            // Headers and Titles
            ["CreateNew"] = new() { ["en"] = "Create New", ["ar"] = "إنشاء جديد" },
            ["CreateNewProject"] = new() { ["en"] = "Create New Project", ["ar"] = "إنشاء مشروع جديد" },
            ["CreateNewMainCriterian"] = new() { ["en"] = "Create New Main Criterian", ["ar"] = "إنشاء معيار رئيسي جديد" },
            ["CreateNewSubCriterian"] = new() { ["en"] = "Create New Sub Criterian", ["ar"] = "إنشاء معيار فرعي جديد" },
            ["ProjectsList"] = new() { ["en"] = "Projects List", ["ar"] = "قائمة المشاريع" },
            ["MainCriteriansList"] = new() { ["en"] = "Main Criterians List", ["ar"] = "قائمة المعايير الرئيسية" },
            ["SubCriteriansList"] = new() { ["en"] = "Sub Criterians List", ["ar"] = "قائمة المعايير الفرعية" },

            // Messages
            ["AreYouSureDelete"] = new() { ["en"] = "Are you sure you want to delete this?", ["ar"] = "هل أنت متأكد من حذف هذا العنصر؟" },
            ["NoItemsFound"] = new() { ["en"] = "No items found", ["ar"] = "لا توجد عناصر" },
            ["ConfirmDelete"] = new() { ["en"] = "Confirm Delete", ["ar"] = "تأكيد الحذف" },

            // Evaluation
            ["Evaluate"] = new() { ["en"] = "Evaluate", ["ar"] = "تقييم" },
            ["Evaluation"] = new() { ["en"] = "Evaluation", ["ar"] = "التقييم" },
            ["EvaluationSummary"] = new() { ["en"] = "Evaluation Summary", ["ar"] = "ملخص التقييم" },
            ["ProjectEvaluation"] = new() { ["en"] = "Project Evaluation", ["ar"] = "تقييم المشروع" },
            ["SaveEvaluation"] = new() { ["en"] = "Save Evaluation", ["ar"] = "حفظ التقييم" },

            // Languages
            ["English"] = new() { ["en"] = "English", ["ar"] = "الإنجليزية" },
            ["Arabic"] = new() { ["en"] = "Arabic", ["ar"] = "العربية" },

            // Main Criterians specific
            ["MainCriterianDetails"] = new() { ["en"] = "Main Criterian Details", ["ar"] = "تفاصيل المعيار الرئيسي" },
            ["EditMainCriterian"] = new() { ["en"] = "Edit Main Criterian", ["ar"] = "تعديل المعيار الرئيسي" },
            ["DeleteMainCriterian"] = new() { ["en"] = "Delete Main Criterian", ["ar"] = "حذف المعيار الرئيسي" },
            ["AddSubCriterian"] = new() { ["en"] = "Add Sub Criterian", ["ar"] = "إضافة معيار فرعي" },
            ["NoSubCriteriansFound"] = new() { ["en"] = "No sub criterians found", ["ar"] = "لا توجد معايير فرعية" },

            // Sub Criterians specific
            ["SubCriterianDetails"] = new() { ["en"] = "Sub Criterian Details", ["ar"] = "تفاصيل المعيار الفرعي" },
            ["EditSubCriterian"] = new() { ["en"] = "Edit Sub Criterian", ["ar"] = "تعديل المعيار الفرعي" },
            ["DeleteSubCriterian"] = new() { ["en"] = "Delete Sub Criterian", ["ar"] = "حذف المعيار الفرعي" },
            ["MainCriterianId"] = new() { ["en"] = "Main Criterian", ["ar"] = "المعيار الرئيسي" },
            ["BackToMainCriterian"] = new() { ["en"] = "Back to Main Criterian", ["ar"] = "رجوع إلى المعيار الرئيسي" },

            // Form helpers
            ["EnterArabicText"] = new() { ["en"] = "Enter Arabic text", ["ar"] = "أدخل النص بالعربية" },
            ["EnterEnglishText"] = new() { ["en"] = "Enter English text", ["ar"] = "أدخل النص بالإنجليزية" },
            ["WeightExample"] = new() { ["en"] = "Example: 0.269", ["ar"] = "مثال: 0.269" },
            ["WeightRange"] = new() { ["en"] = "Value between 0 and 1", ["ar"] = "قيمة بين 0 و 1" },
            ["Required"] = new() { ["en"] = "Required", ["ar"] = "مطلوب" },

            // Status messages
            ["CreatedSuccessfully"] = new() { ["en"] = "Created successfully", ["ar"] = "تم الإنشاء بنجاح" },
            ["UpdatedSuccessfully"] = new() { ["en"] = "Updated successfully", ["ar"] = "تم التحديث بنجاح" },
            ["DeletedSuccessfully"] = new() { ["en"] = "Deleted successfully", ["ar"] = "تم الحذف بنجاح" },
            ["ErrorOccurred"] = new() { ["en"] = "An error occurred", ["ar"] = "حدث خطأ" },

            // Table headers
            ["ID"] = new() { ["en"] = "ID", ["ar"] = "المعرف" },
            ["Name"] = new() { ["en"] = "Name", ["ar"] = "الاسم" },
            ["Status"] = new() { ["en"] = "Status", ["ar"] = "الحالة" },

            // Additional helpful translations
            ["Warning"] = new() { ["en"] = "Warning", ["ar"] = "تحذير" },
            ["Information"] = new() { ["en"] = "Information", ["ar"] = "معلومات" },
            ["QuickStats"] = new() { ["en"] = "Quick Stats", ["ar"] = "معلومات إحصائية" },
            ["WillBeDeleted"] = new() { ["en"] = "Will be deleted", ["ar"] = "سيتم حذف" },
            ["ThisActionCannotBeUndone"] = new() { ["en"] = "This action cannot be undone", ["ar"] = "هذا الإجراء لا يمكن التراجع عنه" },
            ["ForMainCriterian"] = new() { ["en"] = "For Main Criterian", ["ar"] = "للمعيار الرئيسي" },
            ["FromMainCriterian"] = new() { ["en"] = "From Main Criterian", ["ar"] = "من المعيار الرئيسي" },

            // Success/Error messages for forms
            ["MainCriterianCreated"] = new() { ["en"] = "Main criterian created successfully", ["ar"] = "تم إنشاء المعيار الرئيسي بنجاح" },
            ["MainCriterianUpdated"] = new() { ["en"] = "Main criterian updated successfully", ["ar"] = "تم تحديث المعيار الرئيسي بنجاح" },
            ["MainCriterianDeleted"] = new() { ["en"] = "Main criterian deleted successfully", ["ar"] = "تم حذف المعيار الرئيسي بنجاح" },
            ["SubCriterianCreated"] = new() { ["en"] = "Sub criterian created successfully", ["ar"] = "تم إنشاء المعيار الفرعي بنجاح" },
            ["SubCriterianUpdated"] = new() { ["en"] = "Sub criterian updated successfully", ["ar"] = "تم تحديث المعيار الفرعي بنجاح" },
            ["SubCriterianDeleted"] = new() { ["en"] = "Sub criterian deleted successfully", ["ar"] = "تم حذف المعيار الفرعي بنجاح" },
        };

        public static string Translate(string key, string language = "en")
        {
            if (Translations.TryGetValue(key, out var translations))
            {
                return translations.GetValueOrDefault(language, key);
            }
            return key;
        }

        public static string GetLanguageDirection(string language)
        {
            return language == "ar" ? "rtl" : "ltr";
        }

        public static bool IsArabic(string language)
        {
            return language == "ar";
        }
    }
}