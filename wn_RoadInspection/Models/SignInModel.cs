using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using wn_RoadInspection;




namespace wn_web.Models
{
    public class SignInModel
    {
        private ApplicationSignInManager signInManager;
        private SignInListener signInListener;
        private String email;
        private String password;

        public SignInModel(String email, String password, ApplicationSignInManager manager, SignInListener listener)
        {
            this.signInManager = manager;
            this.signInListener = listener;

            this.email = email;
            this.password = password;
        }

        public async Task startSignIn(){

            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                    signInListener.onSignInSuccess();
                    break;

                case SignInStatus.Failure:
                    signInListener.onSignInFail();
                    break;
            }


        }
    }


    public interface SignInListener{
        void onSignInSuccess();
        void onSignInFail();
    }
}