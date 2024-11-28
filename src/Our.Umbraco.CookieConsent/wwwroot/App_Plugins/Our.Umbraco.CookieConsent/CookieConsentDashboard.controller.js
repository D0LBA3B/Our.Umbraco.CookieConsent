(function () {
    "use strict";

    function controller($http, notificationsService) {
        var vm = this;

        vm.loading = true;
        vm.enums = {
            consentModalLayouts: [
                { value: 'Box', displayName: 'box' },
                { value: 'BoxInline', displayName: 'box inline' },
                { value: 'BoxWide', displayName: 'box wide' },
                { value: 'Cloud', displayName: 'cloud' },
                { value: 'CloudInline', displayName: 'cloud inline' },
                { value: 'Bar', displayName: 'bar' },
                { value: 'BarInline', displayName: 'bar inline' }
            ],
            preferencesModalLayouts: [
                { value: 'Box', displayName: 'box' },
                { value: 'Bar', displayName: 'bar' },
                { value: 'BarWide', displayName: 'bar wide' }
            ],
            consentModalPositions: [
                { value: 'TopLeft', displayName: 'top left' },
                { value: 'TopCenter', displayName: 'top center' },
                { value: 'TopRight', displayName: 'top right' },
                { value: 'MiddleLeft', displayName: 'middle left' },
                { value: 'MiddleCenter', displayName: 'middle center' },
                { value: 'MiddleRight', displayName: 'middle right' },
                { value: 'BottomLeft', displayName: 'bottom left' },
                { value: 'BottomCenter', displayName: 'bottom center' },
                { value: 'BottomRight', displayName: 'bottom right' }
            ],
            preferencesModalPositions: [
                { value: 'Left', displayName: 'left' },
                { value: 'Right', displayName: 'right' }
            ]
        };
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
