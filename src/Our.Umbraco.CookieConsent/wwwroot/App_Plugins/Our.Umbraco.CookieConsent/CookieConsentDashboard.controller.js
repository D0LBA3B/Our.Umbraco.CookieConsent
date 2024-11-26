(function () {
    "use strict";

    function controller($http, notificationsService) {
        var vm = this;

        vm.loading = true;
        vm.settings = {
            categories: {
                necessary: true,
                functionality: false,
                analytics: false,
                marketing: false
            },
            translations: {
                ar: false,
                de: false,
                en: true,
                es: false,
                fr: false,
                it: false
            },
            defaultLanguage: 'en',
            gui: {
                modalLayout: 'box',
                modalPosition: 'bottom left',
                flipButtons: false,
                preferencesLayout: 'box',
                preferencesPosition: 'right',
                flipButtonsPreferences: false
            },
            misc: {
                enableDarkMode: false,
                disableTransitions: false,
                disablePageInteraction: false
            },
            theme: 'light'
        };

        vm.loadSettings = function () {
            $http.get('backoffice/api/CookieConsent/GetSettings')
                .then(function (response) {
                    vm.settings = response.data;
                    vm.loading = false;
                })
                .catch(function () {
                    notificationsService.error("Error", "Failed to load settings.");
                    vm.loading = false;
                });
        };

        vm.saveSettings = function () {
            vm.loading = true;
            $http.post('backoffice/api/CookieConsent/SaveSettings', vm.settings)
                .then(function () {
                    notificationsService.success("Success", "Settings saved successfully.");
                    vm.loading = false;
                })
                .catch(function () {
                    notificationsService.error("Error", "Failed to save settings.");
                    vm.loading = false;
                });
        };

        vm.resetConfig = function () {
            vm.loading = true;
            $http.get('backoffice/api/CookieConsent/ResetSettings')
                .then(function (response) {
                    vm.settings = response.data;
                    notificationsService.success("Success", "Settings reset to defaults.");
                    vm.loading = false;
                })
                .catch(function () {
                    notificationsService.error("Error", "Failed to reset settings.");
                    vm.loading = false;
                });
        };

        vm.loadSettings();
        return vm;
    }
    angular.module("umbraco").controller("CookieConsentDashboard.Controller", ['$http', 'notificationsService', controller]);
})();
