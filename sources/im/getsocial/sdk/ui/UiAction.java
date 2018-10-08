package im.getsocial.sdk.ui;

public enum UiAction {
    OPEN_COMMENTS,
    POST_ACTIVITY,
    POST_COMMENT,
    LIKE_ACTIVITY,
    LIKE_COMMENT;

    public interface Pending {
        void proceed();
    }
}
