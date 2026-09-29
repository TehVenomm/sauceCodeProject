.class public Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;
.super Landroid/app/Fragment;
.source "NewsArticleFragment.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;
    }
.end annotation


# static fields
.field private static final KEY_ARTICLE:Ljava/lang/String; = "article"


# instance fields
.field private article:Lnet/gogame/gowrap/model/news/Article;

.field private articleContainer:Landroid/widget/LinearLayout;

.field private dateTimeTextView:Landroid/widget/TextView;

.field private downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

.field private progressBar:Landroid/widget/ProgressBar;

.field private uiContext:Lnet/gogame/gowrap/ui/UIContext;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 39
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    .line 48
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;)Landroid/widget/ProgressBar;
    .locals 0

    .line 39
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->progressBar:Landroid/widget/ProgressBar;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V
    .locals 0

    .line 39
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->launchUrl(Ljava/lang/String;)V

    return-void
.end method

.method public static create(Lnet/gogame/gowrap/model/news/Article;)Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;
    .locals 3

    .line 66
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;-><init>()V

    .line 67
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "article"

    .line 68
    invoke-virtual {v1, v2, p0}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    .line 69
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private launchUrl(Ljava/lang/String;)V
    .locals 2

    if-nez p1, :cond_0

    return-void

    .line 295
    :cond_0
    :try_start_0
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->handleCustomUri(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    goto :goto_0

    .line 298
    :cond_1
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 301
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method private populate()V
    .locals 5

    .line 118
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    invoke-virtual {v0}, Landroid/widget/LinearLayout;->removeAllViews()V

    .line 120
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    if-nez v0, :cond_0

    return-void

    .line 124
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object v0

    if-eqz v0, :cond_1

    const/4 v0, 0x2

    .line 125
    invoke-static {v0, v0}, Ljava/text/SimpleDateFormat;->getDateTimeInstance(II)Ljava/text/DateFormat;

    move-result-object v0

    .line 127
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->dateTimeTextView:Landroid/widget/TextView;

    new-instance v2, Ljava/util/Date;

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    invoke-virtual {v3}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-direct {v2, v3, v4}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v0, v2}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 130
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Article;->getContent()Lnet/gogame/gowrap/model/news/MarkupElement;

    move-result-object v0

    if-eqz v0, :cond_8

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    .line 131
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Article;->getContent()Lnet/gogame/gowrap/model/news/MarkupElement;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/MarkupElement;->getChildren()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_8

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    .line 132
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Article;->getContent()Lnet/gogame/gowrap/model/news/MarkupElement;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v0

    const-string v1, "body"

    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    goto :goto_1

    .line 136
    :cond_2
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/Article;->getContent()Lnet/gogame/gowrap/model/news/MarkupElement;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/news/MarkupElement;->getChildren()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_3
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_7

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/model/news/MarkupElement;

    if-eqz v1, :cond_3

    .line 138
    invoke-virtual {v1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v2

    const-string v3, "paragraph"

    invoke-static {v2, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_4

    .line 139
    invoke-direct {p0, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->populateParagraph(Lnet/gogame/gowrap/model/news/MarkupElement;)V

    goto :goto_0

    .line 140
    :cond_4
    invoke-virtual {v1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v2

    const-string v3, "image"

    invoke-static {v2, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_5

    .line 141
    invoke-direct {p0, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->populateImage(Lnet/gogame/gowrap/model/news/MarkupElement;)V

    goto :goto_0

    .line 142
    :cond_5
    invoke-virtual {v1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v2

    const-string v3, "button"

    invoke-static {v2, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_6

    .line 143
    invoke-direct {p0, v1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->populateButton(Lnet/gogame/gowrap/model/news/MarkupElement;)V

    goto :goto_0

    :cond_6
    const-string v2, "goWrap"

    .line 145
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Unexpected element "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v2, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_7
    return-void

    :cond_8
    :goto_1
    return-void
.end method

.method private populateButton(Lnet/gogame/gowrap/model/news/MarkupElement;)V
    .locals 4

    .line 257
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    const-string v1, "layout_inflater"

    invoke-virtual {v0, v1}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/LayoutInflater;

    .line 260
    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_news_article_button:I

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    const/4 v3, 0x0

    invoke-virtual {v0, v1, v2, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/Button;

    .line 263
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    invoke-virtual {v1, v0}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    .line 265
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getStyle()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 267
    :try_start_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getStyle()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v1

    .line 268
    invoke-virtual {v0}, Landroid/widget/Button;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v2

    invoke-static {v2, v1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->setLevel(Landroid/graphics/drawable/Drawable;I)V

    .line 270
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object v2

    invoke-virtual {v2}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lnet/gogame/gowrap/R$drawable;->net_gogame_gowrap_news_article_button_icon:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v2

    .line 272
    invoke-static {v2, v1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->setLevel(Landroid/graphics/drawable/Drawable;I)V

    const/4 v1, 0x0

    .line 273
    invoke-virtual {v0, v2, v1, v1, v1}, Landroid/widget/Button;->setCompoundDrawablesWithIntrinsicBounds(Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    .line 274
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    const/high16 v2, 0x41000000    # 8.0f

    invoke-static {v1, v2}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v1

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setCompoundDrawablePadding(I)V
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0

    .line 279
    :catch_0
    :cond_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setText(Ljava/lang/CharSequence;)V

    .line 280
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getLink()Ljava/lang/String;

    move-result-object p1

    .line 281
    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;

    invoke-direct {v1, p0, p1}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method private populateImage(Lnet/gogame/gowrap/model/news/MarkupElement;)V
    .locals 4

    .line 236
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getSrc()Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 240
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    const-string v1, "layout_inflater"

    invoke-virtual {v0, v1}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/LayoutInflater;

    .line 243
    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_news_article_image:I

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    const/4 v3, 0x0

    invoke-virtual {v0, v1, v2, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 246
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    invoke-virtual {v1, v0}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    .line 248
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v1, :cond_1

    .line 249
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v1}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v1

    .line 251
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getSrc()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->newBuilder(Ljava/lang/String;)Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;

    move-result-object p1

    new-instance v2, Lnet/gogame/gowrap/ui/download/ImageViewTarget;

    invoke-direct {v2, v0}, Lnet/gogame/gowrap/ui/download/ImageViewTarget;-><init>(Landroid/widget/ImageView;)V

    .line 252
    invoke-virtual {p1, v2}, Lnet/gogame/gowrap/support/DownloadManager$Request$Builder;->into(Lnet/gogame/gowrap/support/DownloadManager$Target;)Lnet/gogame/gowrap/support/DownloadManager$Request;

    move-result-object p1

    .line 249
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/support/DownloadManager;->download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V

    :cond_1
    return-void
.end method

.method private populateParagraph(Lnet/gogame/gowrap/model/news/MarkupElement;)V
    .locals 10

    .line 152
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    const-string v1, "layout_inflater"

    invoke-virtual {v0, v1}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/view/LayoutInflater;

    .line 155
    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_news_article_paragraph:I

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    const/4 v3, 0x0

    invoke-virtual {v0, v1, v2, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    .line 158
    invoke-static {}, Landroid/text/method/LinkMovementMethod;->getInstance()Landroid/text/method/MovementMethod;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setMovementMethod(Landroid/text/method/MovementMethod;)V

    .line 159
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    invoke-virtual {v1, v0}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    .line 163
    new-instance v1, Ljava/lang/StringBuffer;

    invoke-direct {v1}, Ljava/lang/StringBuffer;-><init>()V

    .line 164
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getChildren()Ljava/util/List;

    move-result-object v2

    if-eqz v2, :cond_4

    .line 165
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getChildren()Ljava/util/List;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_0
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_4

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lnet/gogame/gowrap/model/news/MarkupElement;

    if-eqz v4, :cond_0

    .line 167
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v5

    const-string v6, "text"

    invoke-static {v5, v6}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_1

    .line 168
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_0

    .line 169
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v4}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    goto :goto_0

    .line 171
    :cond_1
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v5

    const-string v6, "link"

    invoke-static {v5, v6}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_3

    .line 172
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_2

    .line 173
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v4}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    goto :goto_0

    .line 175
    :cond_2
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getLink()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v4}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    goto :goto_0

    :cond_3
    const-string v5, "goWrap"

    .line 178
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Unexpected element "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v5, v4}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    .line 183
    :cond_4
    new-instance v2, Landroid/text/SpannableString;

    invoke-virtual {v1}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v2, v1}, Landroid/text/SpannableString;-><init>(Ljava/lang/CharSequence;)V

    .line 186
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getChildren()Ljava/util/List;

    move-result-object v1

    if-eqz v1, :cond_c

    .line 189
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/MarkupElement;->getChildren()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x0

    const/4 v4, 0x0

    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_c

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lnet/gogame/gowrap/model/news/MarkupElement;

    if-eqz v5, :cond_b

    .line 191
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v6

    const-string v7, "text"

    invoke-static {v6, v7}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v6

    const/16 v7, 0x11

    if-eqz v6, :cond_8

    .line 192
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_5

    .line 193
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    add-int/2addr v4, v1

    .line 195
    :cond_5
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getTextStyles()Ljava/util/List;

    move-result-object v6

    if-eqz v6, :cond_b

    .line 197
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getTextStyles()Ljava/util/List;

    move-result-object v5

    invoke-interface {v5}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v5

    const/4 v6, 0x0

    :cond_6
    :goto_2
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v8

    if-eqz v8, :cond_7

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    if-eqz v8, :cond_6

    .line 199
    sget-object v9, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$3;->$SwitchMap$net$gogame$gowrap$model$news$MarkupElement$TextStyle:[I

    invoke-virtual {v8}, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ordinal()I

    move-result v8

    aget v8, v9, v8

    packed-switch v8, :pswitch_data_0

    goto :goto_2

    :pswitch_0
    or-int/lit8 v6, v6, 0x2

    goto :goto_2

    :pswitch_1
    or-int/lit8 v6, v6, 0x1

    goto :goto_2

    .line 211
    :cond_7
    new-instance v5, Landroid/text/style/StyleSpan;

    invoke-direct {v5, v6}, Landroid/text/style/StyleSpan;-><init>(I)V

    .line 212
    invoke-virtual {v2, v5, v1, v4, v7}, Landroid/text/SpannableString;->setSpan(Ljava/lang/Object;III)V

    goto :goto_4

    .line 215
    :cond_8
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v6

    const-string v8, "link"

    invoke-static {v6, v8}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v6

    if-eqz v6, :cond_a

    .line 216
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v4

    if-eqz v4, :cond_9

    .line 217
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getText()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    add-int/2addr v4, v1

    goto :goto_3

    .line 219
    :cond_9
    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getLink()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    add-int/2addr v4, v1

    .line 221
    :goto_3
    new-instance v6, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;

    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getLink()Ljava/lang/String;

    move-result-object v5

    invoke-direct {v6, p0, v5}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$ClickableURLSpan;-><init>(Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;Ljava/lang/String;)V

    .line 222
    invoke-virtual {v2, v6, v1, v4, v7}, Landroid/text/SpannableString;->setSpan(Ljava/lang/Object;III)V

    goto :goto_4

    :cond_a
    const-string v1, "goWrap"

    .line 225
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Unexpected element "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Lnet/gogame/gowrap/model/news/MarkupElement;->getType()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v6, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v1, v5}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_b
    :goto_4
    move v1, v4

    goto/16 :goto_1

    .line 232
    :cond_c
    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    .line 75
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    instance-of p3, p3, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p3, :cond_0

    .line 76
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getActivity()Landroid/app/Activity;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/ui/UIContext;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    .line 79
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p3

    if-eqz p3, :cond_1

    .line 80
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p3

    const-string v0, "article"

    invoke-virtual {p3, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/model/news/Article;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->article:Lnet/gogame/gowrap/model/news/Article;

    .line 83
    :cond_1
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_v2017_2_fragment_news_article:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 86
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_article_container:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/LinearLayout;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->articleContainer:Landroid/widget/LinearLayout;

    .line 89
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_article_timestamp:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->dateTimeTextView:Landroid/widget/TextView;

    .line 92
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->populate()V

    .line 94
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_progress_indicator:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ProgressBar;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->progressBar:Landroid/widget/ProgressBar;

    return-object p1
.end method

.method public onPause()V
    .locals 2

    .line 110
    invoke-super {p0}, Landroid/app/Fragment;->onPause()V

    .line 112
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 113
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager;->removeListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V

    :cond_0
    return-void
.end method

.method public onResume()V
    .locals 2

    .line 101
    invoke-super {p0}, Landroid/app/Fragment;->onResume()V

    .line 103
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 104
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;->downloadManagerListener:Lnet/gogame/gowrap/support/DownloadManager$Listener;

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager;->addListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V

    :cond_0
    return-void
.end method
