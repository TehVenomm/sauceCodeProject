package net.gogame.gowrap.ui.v2017_2;

import android.app.Fragment;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.text.SpannableString;
import android.text.method.LinkMovementMethod;
import android.text.style.ClickableSpan;
import android.text.style.StyleSpan;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import java.text.SimpleDateFormat;
import java.util.Date;
import jp.colopl.drapro.LocalNotificationAlarmReceiver;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.model.news.Article;
import net.gogame.gowrap.model.news.MarkupElement;
import net.gogame.gowrap.model.news.MarkupElement.TextStyle;
import net.gogame.gowrap.support.DownloadManager.Listener;
import net.gogame.gowrap.support.DownloadManager.Request.Builder;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.UIContext;
import net.gogame.gowrap.ui.download.ImageViewTarget;
import net.gogame.gowrap.ui.utils.DisplayUtils;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;

public class NewsArticleFragment extends Fragment {
    private static final String KEY_ARTICLE = "article";
    private Article article;
    private LinearLayout articleContainer;
    private TextView dateTimeTextView;
    private Listener downloadManagerListener = new C15431();
    private ProgressBar progressBar;
    private UIContext uiContext;

    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsArticleFragment$1 */
    class C15431 implements Listener {
        C15431() {
        }

        public void onDownloadsStarted() {
            if (NewsArticleFragment.this.progressBar != null) {
                NewsArticleFragment.this.progressBar.setVisibility(0);
            }
        }

        public void onDownloadsFinished() {
            if (NewsArticleFragment.this.progressBar != null) {
                NewsArticleFragment.this.progressBar.setVisibility(8);
            }
        }
    }

    private class ClickableURLSpan extends ClickableSpan {
        private final String url;

        public ClickableURLSpan(String str) {
            this.url = str;
        }

        public void onClick(View view) {
            NewsArticleFragment.this.launchUrl(this.url);
        }
    }

    public static NewsArticleFragment create(Article article) {
        NewsArticleFragment newsArticleFragment = new NewsArticleFragment();
        Bundle bundle = new Bundle();
        bundle.putSerializable(KEY_ARTICLE, article);
        newsArticleFragment.setArguments(bundle);
        return newsArticleFragment;
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        if (getArguments() != null) {
            this.article = (Article) getArguments().getSerializable(KEY_ARTICLE);
        }
        View inflate = layoutInflater.inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_news_article, viewGroup, false);
        this.articleContainer = (LinearLayout) inflate.findViewById(C1426R.id.net_gogame_gowrap_news_article_container);
        this.dateTimeTextView = (TextView) inflate.findViewById(C1426R.id.net_gogame_gowrap_news_article_timestamp);
        populate();
        this.progressBar = (ProgressBar) inflate.findViewById(C1426R.id.net_gogame_gowrap_progress_indicator);
        return inflate;
    }

    public void onResume() {
        super.onResume();
        if (this.uiContext != null) {
            this.uiContext.getDownloadManager().addListener(this.downloadManagerListener);
        }
    }

    public void onPause() {
        super.onPause();
        if (this.uiContext != null) {
            this.uiContext.getDownloadManager().removeListener(this.downloadManagerListener);
        }
    }

    private void populate() {
        this.articleContainer.removeAllViews();
        if (this.article != null) {
            if (this.article.getDateTime() != null) {
                this.dateTimeTextView.setText(SimpleDateFormat.getDateTimeInstance(2, 2).format(new Date(this.article.getDateTime().longValue())));
            }
            if (this.article.getContent() != null && this.article.getContent().getChildren() != null && StringUtils.isEquals(this.article.getContent().getType(), LocalNotificationAlarmReceiver.EXTRA_BODY)) {
                for (MarkupElement markupElement : this.article.getContent().getChildren()) {
                    if (markupElement != null) {
                        if (StringUtils.isEquals(markupElement.getType(), "paragraph")) {
                            populateParagraph(markupElement);
                        } else if (StringUtils.isEquals(markupElement.getType(), "image")) {
                            populateImage(markupElement);
                        } else if (StringUtils.isEquals(markupElement.getType(), "button")) {
                            populateButton(markupElement);
                        } else {
                            Log.w(Constants.TAG, "Unexpected element " + markupElement.getType());
                        }
                    }
                }
            }
        }
    }

    private LayoutInflater getLayoutInflater() {
        return (LayoutInflater) getActivity().getSystemService("layout_inflater");
    }

    private void populateParagraph(MarkupElement markupElement) {
        TextView textView = (TextView) getLayoutInflater().inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_news_article_paragraph, this.articleContainer, false);
        textView.setMovementMethod(LinkMovementMethod.getInstance());
        this.articleContainer.addView(textView);
        StringBuffer stringBuffer = new StringBuffer();
        if (markupElement.getChildren() != null) {
            for (MarkupElement markupElement2 : markupElement.getChildren()) {
                if (markupElement2 != null) {
                    if (StringUtils.isEquals(markupElement2.getType(), "text")) {
                        if (markupElement2.getText() != null) {
                            stringBuffer.append(markupElement2.getText());
                        }
                    } else if (!StringUtils.isEquals(markupElement2.getType(), "link")) {
                        Log.w(Constants.TAG, "Unexpected element " + markupElement2.getType());
                    } else if (markupElement2.getText() != null) {
                        stringBuffer.append(markupElement2.getText());
                    } else {
                        stringBuffer.append(markupElement2.getLink());
                    }
                }
            }
        }
        CharSequence spannableString = new SpannableString(stringBuffer.toString());
        if (markupElement.getChildren() != null) {
            int i = 0;
            int i2 = 0;
            for (MarkupElement markupElement22 : markupElement.getChildren()) {
                int i3;
                if (markupElement22 != null) {
                    if (StringUtils.isEquals(markupElement22.getType(), "text")) {
                        if (markupElement22.getText() != null) {
                            i = markupElement22.getText().length() + i2;
                        }
                        if (markupElement22.getTextStyles() != null) {
                            int i4 = 0;
                            for (TextStyle textStyle : markupElement22.getTextStyles()) {
                                if (textStyle != null) {
                                    switch (textStyle) {
                                        case BOLD:
                                            i3 = i4 | 1;
                                            break;
                                        case ITALIC:
                                            i3 = i4 | 2;
                                            break;
                                    }
                                }
                                i3 = i4;
                                i4 = i3;
                            }
                            spannableString.setSpan(new StyleSpan(i4), i2, i, 17);
                            i3 = i;
                            i = i3;
                            i2 = i3;
                        }
                    } else if (StringUtils.isEquals(markupElement22.getType(), "link")) {
                        if (markupElement22.getText() != null) {
                            i = markupElement22.getText().length() + i2;
                        } else {
                            i = markupElement22.getLink().length() + i2;
                        }
                        spannableString.setSpan(new ClickableURLSpan(markupElement22.getLink()), i2, i, 17);
                        i3 = i;
                        i = i3;
                        i2 = i3;
                    } else {
                        Log.w(Constants.TAG, "Unexpected element " + markupElement22.getType());
                    }
                }
                i3 = i;
                i = i3;
                i2 = i3;
            }
        }
        textView.setText(spannableString);
    }

    private void populateImage(MarkupElement markupElement) {
        if (markupElement.getSrc() != null) {
            ImageView imageView = (ImageView) getLayoutInflater().inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_news_article_image, this.articleContainer, false);
            this.articleContainer.addView(imageView);
            if (this.uiContext != null) {
                this.uiContext.getDownloadManager().download(Builder.newBuilder(markupElement.getSrc()).into(new ImageViewTarget(imageView)));
            }
        }
    }

    private void populateButton(MarkupElement markupElement) {
        Button button = (Button) getLayoutInflater().inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_news_article_button, this.articleContainer, false);
        this.articleContainer.addView(button);
        if (markupElement.getStyle() != null) {
            try {
                int parseInt = Integer.parseInt(markupElement.getStyle());
                DisplayUtils.setLevel(button.getBackground(), parseInt);
                Drawable drawable = getActivity().getResources().getDrawable(C1426R.drawable.net_gogame_gowrap_news_article_button_icon);
                DisplayUtils.setLevel(drawable, parseInt);
                button.setCompoundDrawablesWithIntrinsicBounds(drawable, null, null, null);
                button.setCompoundDrawablePadding(DisplayUtils.pxFromDp(getActivity(), 8.0f));
            } catch (NumberFormatException e) {
            }
        }
        button.setText(markupElement.getText());
        final String link = markupElement.getLink();
        button.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                NewsArticleFragment.this.launchUrl(link);
            }
        });
    }

    private void launchUrl(String str) {
        if (str != null) {
            try {
                if (!GoWrapImpl.INSTANCE.handleCustomUri(str)) {
                    ExternalAppLauncher.openUrlInExternalBrowser(getActivity(), str);
                }
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }
}
