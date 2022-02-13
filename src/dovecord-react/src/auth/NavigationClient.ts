import { NavigationClient, NavigationOptions } from "@azure/msal-browser";
import { NavigateFunction } from "react-router-dom";

/**
 * This is an example for overriding the default function MSAL uses to navigate to other urls in your webpage
 */
/**
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/issues/4422
 * react-router-dom v6 implementation of msal NavigationClient
 * @see https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/3d1cbe7599bbc00da12159a1c0c5e4aa720fb7a6/samples/msal-react-samples/react-router-sample/src/utils/NavigationClient.js#L6
 */
export class CustomNavigationClient extends NavigationClient {
    constructor(private navigate: NavigateFunction) {
    super();
    }

    /**
   * Navigates to other pages within the same web application
   * You can use the useHistory hook provided by react-router-dom to take advantage of client-side routing
   * @param url
   * @param options
   */
    async navigateInternal(url: string, options: NavigationOptions) {
        const relativePath = url.replace(window.location.origin, "");
        if (options.noHistory) {
            this.navigate(relativePath, { replace: true });
        } else {
            this.navigate(relativePath);
        }
    return false;
    }
}
