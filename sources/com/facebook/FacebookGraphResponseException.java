package com.facebook;

public class FacebookGraphResponseException extends FacebookException {
    private final GraphResponse graphResponse;

    public FacebookGraphResponseException(GraphResponse graphResponse2, String str) {
        super(str);
        this.graphResponse = graphResponse2;
    }

    public final GraphResponse getGraphResponse() {
        return this.graphResponse;
    }

    public final String toString() {
        FacebookRequestError facebookRequestError = this.graphResponse != null ? this.graphResponse.getError() : null;
        StringBuilder append = new StringBuilder().append("{FacebookGraphResponseException: ");
        String message = getMessage();
        if (message != null) {
            append.append(message);
            append.append(" ");
        }
        if (facebookRequestError != null) {
            append.append("httpResponseCode: ").append(facebookRequestError.getRequestStatusCode()).append(", facebookErrorCode: ").append(facebookRequestError.getErrorCode()).append(", facebookErrorType: ").append(facebookRequestError.getErrorType()).append(", message: ").append(facebookRequestError.getErrorMessage()).append("}");
        }
        return append.toString();
    }
}
