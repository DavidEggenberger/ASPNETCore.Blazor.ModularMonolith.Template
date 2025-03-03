﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Modules.LandingPages.Web.Components;
using Shared.Client.Components.Modals;

namespace Modules.LandingPages.Web
{
    public partial class LandingPagesLayout : LayoutComponentBase
    {
        [Inject]
        public IModalService ModalService { get; set; }

        private IModalReference modalReference;

        private bool collapseNavMenu = true;
        private string NavMenuCssClass => collapseNavMenu ? "mobile:hidden" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        public void OpenSignInModal()
        {
            //var modelParameters = new ModalParameters
            //{
            //    { nameof(SignInModal.CancelRequestedCallback), new EventCallback(this, () => modalReference.Close()) }
            //};

            //modalReference = ModalService.Show<SignInModal>(string.Empty, modelParameters, DefaultModalOptions.Options);
        }

        public void OpenSignUpModal()
        {
            var modelParameters = new ModalParameters
            {
                { nameof(SignInModal.CancelRequestedCallback), new EventCallback(null, () => modalReference.Close()) }
            };

            modalReference = ModalService.Show<SignUpModal>(string.Empty, modelParameters, DefaultModalOptions.Modal);
        }
    }
}
