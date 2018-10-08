package im.getsocial.sdk.ui.internal.views;

import android.annotation.TargetApi;
import android.content.Context;
import android.text.Layout;
import android.text.Spannable;
import android.text.SpannableString;
import android.text.SpannableStringBuilder;
import android.text.TextPaint;
import android.text.method.LinkMovementMethod;
import android.text.style.BackgroundColorSpan;
import android.text.style.ClickableSpan;
import android.text.style.ForegroundColorSpan;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.Mention;
import im.getsocial.sdk.activities.MentionTypes;
import im.getsocial.sdk.activities.PostAuthor;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.sharedl10n.Localization;
import im.getsocial.sdk.sharedl10n.generated.LanguageStrings;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p125h.ruWsnwUPKh;
import im.getsocial.sdk.ui.internal.p131d.p132a.zoToeBNOjF;
import im.getsocial.sdk.ui.internal.views.recycling.Recyclable;
import java.text.SimpleDateFormat;
import java.util.List;
import java.util.Locale;
import java.util.concurrent.TimeUnit;
import java.util.regex.Matcher;

public class ActivityContainerView extends RelativeLayout implements OnClickListener, Recyclable {
    /* renamed from: b */
    private static final ThreadLocal<SimpleDateFormat> f3082b = new C11931();
    /* renamed from: c */
    private static final long f3083c = TimeUnit.SECONDS.toMillis(1);
    /* renamed from: d */
    private static final long f3084d = TimeUnit.SECONDS.toMillis(30);
    /* renamed from: e */
    private static final long f3085e = TimeUnit.MINUTES.toMillis(1);
    /* renamed from: f */
    private static final long f3086f = TimeUnit.HOURS.toMillis(1);
    /* renamed from: g */
    private static final long f3087g = TimeUnit.DAYS.toMillis(1);
    /* renamed from: h */
    private static final long f3088h = TimeUnit.DAYS.toMillis(7);
    /* renamed from: i */
    private static final long f3089i = TimeUnit.DAYS.toMillis(21);
    @XdbacJlTDQ
    /* renamed from: a */
    Localization f3090a;
    /* renamed from: j */
    private ImageView f3091j;
    /* renamed from: k */
    private TextView f3092k;
    /* renamed from: l */
    private AssetButton f3093l;
    /* renamed from: m */
    private TextView f3094m;
    /* renamed from: n */
    private TextView f3095n;
    /* renamed from: o */
    private View f3096o;
    /* renamed from: p */
    private MediaContentView f3097p;
    /* renamed from: q */
    private AssetButton f3098q;
    /* renamed from: r */
    private MultiTextView f3099r;
    /* renamed from: s */
    private MultiTextView f3100s;
    /* renamed from: t */
    private AssetButton f3101t;
    /* renamed from: u */
    private View f3102u;
    /* renamed from: v */
    private KluUZYuxme f3103v;
    /* renamed from: w */
    private ActivityPost f3104w;
    /* renamed from: x */
    private int f3105x;
    /* renamed from: y */
    private OnActivityEventListener f3106y;

    public static class OnActivityEventListener {
        protected OnActivityEventListener() {
        }

        /* renamed from: a */
        public void mo4608a() {
        }

        /* renamed from: a */
        public void mo4609a(String str) {
        }

        /* renamed from: b */
        public void mo4610b() {
        }

        /* renamed from: b */
        public void mo4611b(String str) {
        }

        /* renamed from: c */
        public void mo4707c() {
        }

        /* renamed from: d */
        public void mo4612d() {
        }

        /* renamed from: e */
        public void mo4708e() {
        }

        /* renamed from: f */
        public void mo4613f() {
        }

        /* renamed from: g */
        public void mo4614g() {
        }

        /* renamed from: h */
        public void mo4615h() {
        }

        /* renamed from: i */
        public void mo4709i() {
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.ActivityContainerView$1 */
    static final class C11931 extends ThreadLocal<SimpleDateFormat> {
        C11931() {
        }

        protected final /* synthetic */ Object initialValue() {
            return new SimpleDateFormat("dd/MM/yy", Locale.getDefault());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.ActivityContainerView$3 */
    class C11953 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ ActivityContainerView f3077a;

        C11953(ActivityContainerView activityContainerView) {
            this.f3077a = activityContainerView;
        }

        public void onClick(View view) {
            this.f3077a.f3106y.mo4709i();
        }
    }

    private class jjbQypPegg extends ClickableSpan {
        /* renamed from: a */
        final /* synthetic */ ActivityContainerView f3078a;
        /* renamed from: b */
        private final Mention f3079b;

        jjbQypPegg(ActivityContainerView activityContainerView, Mention mention) {
            this.f3078a = activityContainerView;
            this.f3079b = mention;
        }

        public void onClick(View view) {
            String type = this.f3079b.getType();
            if (MentionTypes.USER.equals(type)) {
                this.f3078a.f3106y.mo4609a(this.f3079b.getUserId());
            } else {
                this.f3078a.f3106y.mo4609a(type);
            }
        }

        public void updateDrawState(TextPaint textPaint) {
            super.updateDrawState(textPaint);
            textPaint.setUnderlineText(false);
        }
    }

    private class upgqDBbsrL extends ClickableSpan {
        /* renamed from: a */
        final /* synthetic */ ActivityContainerView f3080a;
        /* renamed from: b */
        private final String f3081b;

        upgqDBbsrL(ActivityContainerView activityContainerView, String str) {
            this.f3080a = activityContainerView;
            this.f3081b = str;
        }

        public void onClick(View view) {
            this.f3080a.f3106y.mo4611b(this.f3081b.substring(1));
        }

        public void updateDrawState(TextPaint textPaint) {
            super.updateDrawState(textPaint);
            textPaint.setUnderlineText(false);
        }
    }

    public ActivityContainerView(Context context) {
        super(context);
        m3473e();
    }

    public ActivityContainerView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        m3473e();
    }

    public ActivityContainerView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        m3473e();
    }

    @TargetApi(21)
    public ActivityContainerView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        m3473e();
    }

    /* renamed from: a */
    private void m3468a(Spannable spannable) {
        zoToeBNOjF a = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3132p().m3145a();
        zoToeBNOjF c = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3248a(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3126j().m3209a()).m3156c();
        Matcher matcher = im.getsocial.sdk.ui.activities.p116a.p122h.jjbQypPegg.f2752a.matcher(spannable);
        while (matcher.find()) {
            int start = matcher.start();
            int end = matcher.end();
            if (end - start <= 81) {
                spannable.setSpan(new BackgroundColorSpan(a.m3215a()), start, end, 33);
                spannable.setSpan(new upgqDBbsrL(this, matcher.group()), start, end, 33);
                spannable.setSpan(new ForegroundColorSpan(c.m3215a()), start, end, 33);
            }
        }
    }

    /* renamed from: a */
    private void m3469a(Spannable spannable, List<Mention> list) {
        zoToeBNOjF a = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3132p().m3145a();
        zoToeBNOjF c = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3248a(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3126j().m3209a()).m3156c();
        for (Mention mention : list) {
            Object obj = (mention.getEndIndex() <= mention.getStartIndex() || spannable.length() < mention.getEndIndex()) ? null : 1;
            if (obj != null) {
                int startIndex = mention.getStartIndex();
                int endIndex = mention.getEndIndex();
                spannable.setSpan(new BackgroundColorSpan(a.m3215a()), startIndex, endIndex, 33);
                spannable.setSpan(new jjbQypPegg(this, mention), startIndex, endIndex, 33);
                spannable.setSpan(new ForegroundColorSpan(c.m3215a()), startIndex, endIndex, 33);
            }
        }
    }

    /* renamed from: e */
    private void m3473e() {
        ztWNWCuZiM.m1221a((Object) this);
        inflate(getContext(), C1067R.layout.item_activity_post, this);
        this.f3103v = KluUZYuxme.m3299a(getContext());
        this.f3091j = (ImageView) findViewById(C1067R.id.image_view_user_avatar);
        this.f3091j.setContentDescription("user_avatar");
        this.f3092k = (TextView) findViewById(C1067R.id.text_view_user_name);
        this.f3092k.setContentDescription("user_name");
        this.f3093l = (AssetButton) findViewById(C1067R.id.button_more);
        this.f3093l.setContentDescription("more_button");
        this.f3094m = (TextView) findViewById(C1067R.id.text_view_activity_text);
        this.f3094m.setContentDescription("post_content_text");
        this.f3095n = (TextView) findViewById(C1067R.id.text_view_posted_time);
        this.f3095n.setContentDescription("posted_time");
        this.f3096o = findViewById(C1067R.id.container_extras);
        this.f3097p = (MediaContentView) findViewById(C1067R.id.media_content);
        this.f3097p.setContentDescription("activity_media_content");
        this.f3098q = (AssetButton) findViewById(C1067R.id.button_action);
        this.f3098q.setContentDescription("post_content_button");
        this.f3099r = (MultiTextView) findViewById(C1067R.id.text_view_comments);
        this.f3099r.setContentDescription("comment_count");
        this.f3100s = (MultiTextView) findViewById(C1067R.id.text_view_likes);
        this.f3100s.setContentDescription("likes_count");
        this.f3101t = (AssetButton) findViewById(C1067R.id.button_like);
        this.f3101t.setContentDescription("like_button");
        this.f3102u = findViewById(C1067R.id.divider);
        this.f3103v.m3306a(this.f3102u, true);
        this.f3103v.m3312a(this.f3098q);
        this.f3103v.m3309a(this.f3092k);
        this.f3103v.m3325c(this.f3094m);
        this.f3103v.m3322b(this.f3095n);
        this.f3097p.m3537a(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3115D().m3168a().m3214a());
        this.f3094m.setMaxLines(1);
        jjbQypPegg.m3595a(this.f3096o, 0, 0, 0, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3253b(10.0f));
        this.f3094m.setClickable(true);
        this.f3094m.setMovementMethod(LinkMovementMethod.getInstance());
        this.f3094m.setOnTouchListener(new im.getsocial.sdk.ui.internal.p128a.upgqDBbsrL());
    }

    /* renamed from: f */
    private void m3474f() {
        if (!isShown()) {
            this.f3097p.m3536a();
        }
    }

    /* renamed from: g */
    private boolean m3475g() {
        return this.f3104w != null;
    }

    /* renamed from: a */
    public final void m3476a() {
        this.f3103v.m3306a(this.f3102u, false);
    }

    /* renamed from: a */
    public final void m3477a(int i) {
        this.f3105x = i;
        this.f3094m.setMaxLines(i);
    }

    /* renamed from: a */
    public final void m3478a(ActivityPost activityPost) {
        long createdAt = activityPost.getCreatedAt();
        TextView textView = this.f3095n;
        createdAt *= 1000;
        long currentTimeMillis = System.currentTimeMillis() - createdAt;
        LanguageStrings strings = this.f3090a.strings();
        CharSequence format = currentTimeMillis < f3084d ? strings.TimestampJustNow : currentTimeMillis < f3085e ? String.format(strings.TimestampSeconds, new Object[]{Double.valueOf(Math.floor(((double) currentTimeMillis) / ((double) f3083c)))}) : currentTimeMillis < f3086f ? String.format(strings.TimestampMinutes, new Object[]{Double.valueOf(Math.floor(((double) currentTimeMillis) / ((double) f3085e)))}) : currentTimeMillis < f3087g ? String.format(strings.TimestampHours, new Object[]{Double.valueOf(Math.floor(((double) currentTimeMillis) / ((double) f3086f)))}) : currentTimeMillis < f3088h ? String.format(strings.TimestampDays, new Object[]{Double.valueOf(Math.floor(((double) currentTimeMillis) / ((double) f3087g)))}) : currentTimeMillis < f3089i ? String.format(strings.TimestampWeeks, new Object[]{Double.valueOf(Math.floor(((double) currentTimeMillis) / ((double) f3088h)))}) : ((SimpleDateFormat) f3082b.get()).format(Long.valueOf(createdAt));
        textView.setText(format);
        if (!m3475g() || !activityPost.equals(this.f3104w)) {
            if (!(m3475g() && activityPost.getCommentsCount() == this.f3104w.getCommentsCount())) {
                this.f3103v.m3304a(activityPost.getCommentsCount(), this.f3099r);
            }
            if (!(m3475g() && activityPost.getLikesCount() == this.f3104w.getLikesCount())) {
                if (activityPost.getLikesCount() > 0) {
                    int likesCount = activityPost.getLikesCount();
                    this.f3100s.setVisibility(0);
                    this.f3103v.m3321b(likesCount, this.f3100s);
                } else {
                    this.f3100s.setVisibility(8);
                }
            }
            if (!(m3475g() && activityPost.isLikedByMe() == this.f3104w.isLikedByMe())) {
                this.f3103v.m3324b(activityPost.isLikedByMe(), this.f3101t);
            }
            if (!(m3475g() && activityPost.getId().equals(this.f3104w.getId()))) {
                PostAuthor author = activityPost.getAuthor();
                this.f3092k.setText(author.getDisplayName());
                this.f3103v.m3316a(author.getAvatarUrl(), this.f3091j);
                this.f3103v.m3318a(author.isVerified(), this.f3092k);
                this.f3103v.m3319a(!author.isVerified(), this.f3093l);
                if (activityPost.hasText()) {
                    format = activityPost.getText();
                    List mentions = activityPost.getMentions();
                    this.f3094m.setVisibility(0);
                    Spannable spannableString = new SpannableString(format);
                    m3469a(spannableString, mentions);
                    m3468a(spannableString);
                    this.f3094m.setText(spannableString);
                } else {
                    this.f3094m.setVisibility(8);
                }
                if (activityPost.hasVideo()) {
                    this.f3097p.setVisibility(0);
                    this.f3097p.m3539a(activityPost.getImageUrl(), activityPost.getVideoUrl(), new C11953(this));
                } else if (activityPost.hasImage()) {
                    this.f3097p.setVisibility(0);
                    this.f3097p.m3538a(activityPost.getImageUrl());
                } else {
                    this.f3097p.setVisibility(8);
                    m3474f();
                }
                if (activityPost.hasButton()) {
                    this.f3098q.setVisibility(0);
                    this.f3098q.m3503a(activityPost.getButtonTitle());
                    this.f3098q.setTag(activityPost.getButtonAction());
                } else {
                    this.f3098q.setVisibility(8);
                }
            }
            this.f3104w = activityPost;
        }
    }

    /* renamed from: a */
    public final void m3479a(OnActivityEventListener onActivityEventListener) {
        this.f3106y = onActivityEventListener;
        this.f3101t.setOnClickListener(this);
        ruWsnwUPKh.m3364a(this.f3101t);
        this.f3098q.setOnClickListener(this);
        this.f3099r.setOnClickListener(this);
        this.f3100s.setOnClickListener(this);
        this.f3091j.setOnClickListener(this);
        this.f3097p.setOnClickListener(this);
        this.f3093l.setOnClickListener(this);
        this.f3094m.setOnClickListener(this);
        ruWsnwUPKh.m3364a(this.f3093l);
        setOnClickListener(this);
    }

    /* renamed from: b */
    public final void m3480b() {
        this.f3099r.setVisibility(8);
    }

    /* renamed from: c */
    public final void m3481c() {
        this.f3101t.setVisibility(8);
    }

    /* renamed from: d */
    public final void mo4740d() {
        m3474f();
    }

    public void onClick(View view) {
        int id = view.getId();
        if (id == C1067R.id.button_like) {
            this.f3106y.mo4608a();
        } else if (id == C1067R.id.button_action) {
            this.f3106y.mo4610b();
        } else if (id == C1067R.id.text_view_comments) {
            this.f3106y.mo4707c();
        } else if (id == C1067R.id.text_view_likes) {
            this.f3106y.mo4612d();
        } else if (id == C1067R.id.image_view_user_avatar) {
            this.f3106y.mo4613f();
        } else if (id == C1067R.id.image_view_activity_image) {
            this.f3106y.mo4614g();
        } else if (id == C1067R.id.button_more) {
            this.f3106y.mo4615h();
        } else {
            this.f3106y.mo4708e();
        }
    }

    protected void onLayout(boolean z, int i, int i2, int i3, int i4) {
        super.onLayout(z, i, i2, i3, i4);
        Layout layout = this.f3094m.getLayout();
        if (layout != null && layout.getLineCount() > this.f3105x) {
            final CharSequence subSequence = this.f3094m.getText().subSequence(0, layout.getLineEnd(this.f3105x - 1) - 1);
            post(new Runnable(this) {
                /* renamed from: b */
                final /* synthetic */ ActivityContainerView f3076b;

                public void run() {
                    CharSequence spannableStringBuilder = new SpannableStringBuilder(subSequence);
                    spannableStringBuilder.append("…");
                    this.f3076b.f3094m.setText(spannableStringBuilder);
                }
            });
        }
    }
}
