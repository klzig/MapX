Localization notes:

https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/text?tabs=windows
https://github.com/aspnet/LibraryManager/wiki/Using-MAT-for-RESX-to-XLIFF-conversion
https://docs.microsoft.com/en-us/windows/uwp/design/globalizing/mat-faq-troubleshooting
https://multilingualapptoolkit.uservoice.com/knowledgebase/articles/1167898-microsoft-translator-moves-to-the-azure-portal
https://docs.microsoft.com/en-us/azure/cognitive-services/translator/translator-text-how-to-signup

To regenerate all translations (this is really flakey, doesn't always work):
1) Delete language-specific resx files under CTForms.Properties
2) Delete everything under CTForms.MultilingualResources  (maybe not necessary?)
3) On CTForms, right-click Multilingual App Toolkit > Generate Machine Translations
4) Build > Clean Solution
5) Build