package im.getsocial.sdk.ui.activities.p116a.p127e;

import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPost.Builder;

/* renamed from: im.getsocial.sdk.ui.activities.a.e.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static ActivityPost m3065a(ActivityPost activityPost) {
        boolean z = !activityPost.isLikedByMe();
        return jjbQypPegg.m3067c(activityPost).likedByMe(z).likesCount(z ? activityPost.getLikesCount() + 1 : activityPost.getLikesCount() - 1).build();
    }

    /* renamed from: b */
    public static ActivityPost m3066b(ActivityPost activityPost) {
        return jjbQypPegg.m3067c(activityPost).commentsCount(activityPost.getCommentsCount() - 1).build();
    }

    /* renamed from: c */
    private static Builder m3067c(ActivityPost activityPost) {
        return ActivityPost.builder().id(activityPost.getId()).commentsCount(activityPost.getCommentsCount()).author(activityPost.getAuthor()).content(activityPost.getText(), activityPost.getImageUrl(), activityPost.getVideoUrl(), activityPost.getButtonTitle(), activityPost.getButtonAction()).stickyStart(activityPost.getStickyStart()).stickyEnd(activityPost.getStickyEnd()).createdAt(activityPost.getCreatedAt()).likedByMe(activityPost.isLikedByMe()).likesCount(activityPost.getLikesCount()).feedId(activityPost.getFeedId()).mentions(activityPost.getMentions());
    }
}
