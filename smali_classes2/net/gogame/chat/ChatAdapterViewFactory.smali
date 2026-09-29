.class public Lnet/gogame/chat/ChatAdapterViewFactory;
.super Ljava/lang/Object;
.source "ChatAdapterViewFactory.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;,
        Lnet/gogame/chat/ChatAdapterViewFactory$Option;,
        Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;
    }
.end annotation


# instance fields
.field private final allowRatingChange:Z

.field private final context:Landroid/content/Context;

.field private final uiContext:Lnet/gogame/chat/UIContext;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lnet/gogame/chat/UIContext;Z)V
    .locals 0

    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 34
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    .line 35
    iput-object p2, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->uiContext:Lnet/gogame/chat/UIContext;

    .line 36
    iput-boolean p3, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->allowRatingChange:Z

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/chat/ChatAdapterViewFactory;)Landroid/content/Context;
    .locals 0

    .line 24
    iget-object p0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/chat/ChatAdapterViewFactory;)Lnet/gogame/chat/UIContext;
    .locals 0

    .line 24
    iget-object p0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->uiContext:Lnet/gogame/chat/UIContext;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/chat/ChatAdapterViewFactory;)Z
    .locals 0

    .line 24
    iget-boolean p0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->allowRatingChange:Z

    return p0
.end method

.method static synthetic access$300(Lnet/gogame/chat/ChatAdapterViewFactory;)V
    .locals 0

    .line 24
    invoke-direct {p0}, Lnet/gogame/chat/ChatAdapterViewFactory;->showAlreadyRated()V

    return-void
.end method

.method private getDrawable(I)Landroid/graphics/drawable/Drawable;
    .locals 2

    .line 362
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    .line 363
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object v1

    invoke-virtual {v0, p1, v1}, Landroid/content/res/Resources;->getDrawable(ILandroid/content/res/Resources$Theme;)Landroid/graphics/drawable/Drawable;

    move-result-object p1

    return-object p1

    .line 365
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p1

    return-object p1
.end method

.method private getViewType(Landroid/view/View;)Ljava/lang/String;
    .locals 1

    if-eqz p1, :cond_1

    .line 40
    invoke-virtual {p1}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 43
    :cond_0
    invoke-virtual {p1}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object p1

    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method private initAgentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;)Landroid/view/View;
    .locals 3

    .line 82
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {v0}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    const-string v1, "agentMessage"

    .line 83
    invoke-direct {p0, p1, v1}, Lnet/gogame/chat/ChatAdapterViewFactory;->viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z

    move-result v1

    const/4 v2, 0x0

    if-nez v1, :cond_0

    .line 84
    sget p1, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_agent_layout:I

    invoke-virtual {v0, p1, p2, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 88
    :cond_0
    sget p2, Lcom/zopim/android/sdk/R$id;->profileIcon:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Lnet/gogame/chat/RoundedImageView;

    .line 90
    sget v0, Lcom/zopim/android/sdk/R$id;->agentName:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    if-eqz p3, :cond_2

    .line 93
    invoke-virtual {p2, v2}, Lnet/gogame/chat/RoundedImageView;->setVisibility(I)V

    .line 94
    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setVisibility(I)V

    if-eqz p5, :cond_1

    .line 96
    iget-object p3, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p3}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object p3

    invoke-virtual {p3, p5}, Lcom/squareup/picasso/Picasso;->load(Ljava/lang/String;)Lcom/squareup/picasso/RequestCreator;

    move-result-object p3

    sget p5, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_agent_profile_placeholder:I

    .line 97
    invoke-virtual {p3, p5}, Lcom/squareup/picasso/RequestCreator;->placeholder(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object p3

    .line 98
    invoke-virtual {p3, p2}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;)V

    .line 100
    :cond_1
    invoke-virtual {v0, p4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_0

    :cond_2
    const/4 p3, 0x4

    .line 102
    invoke-virtual {p2, p3}, Lnet/gogame/chat/RoundedImageView;->setVisibility(I)V

    const/16 p2, 0x8

    .line 103
    invoke-virtual {v0, p2}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_0
    return-object p1
.end method

.method private showAlreadyRated()V
    .locals 3

    .line 357
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_already_rated_message:I

    const/4 v2, 0x1

    invoke-static {v0, v1, v2}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v0

    .line 358
    invoke-virtual {v0}, Landroid/widget/Toast;->show()V

    return-void
.end method

.method private viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z
    .locals 0

    .line 47
    invoke-direct {p0, p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->getViewType(Landroid/view/View;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, p2}, Lorg/apache/commons/lang3/StringUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result p1

    return p1
.end method


# virtual methods
.method public getAgentAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;
    .locals 0

    if-eqz p6, :cond_1

    if-nez p7, :cond_0

    goto :goto_0

    .line 133
    :cond_0
    invoke-direct/range {p0 .. p5}, Lnet/gogame/chat/ChatAdapterViewFactory;->initAgentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    .line 136
    sget p2, Lcom/zopim/android/sdk/R$id;->agentTextView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    const/16 p3, 0x8

    .line 137
    invoke-virtual {p2, p3}, Landroid/widget/TextView;->setVisibility(I)V

    .line 139
    sget p2, Lcom/zopim/android/sdk/R$id;->cellImageView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    const/4 p3, 0x0

    .line 141
    invoke-virtual {p2, p3}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 142
    iget-object p3, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p3}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object p3

    .line 143
    invoke-virtual {p3, p7}, Lcom/squareup/picasso/Picasso;->load(Ljava/lang/String;)Lcom/squareup/picasso/RequestCreator;

    move-result-object p3

    const/16 p4, 0x190

    .line 144
    invoke-virtual {p3, p4, p4}, Lcom/squareup/picasso/RequestCreator;->resize(II)Lcom/squareup/picasso/RequestCreator;

    move-result-object p3

    .line 145
    invoke-virtual {p3}, Lcom/squareup/picasso/RequestCreator;->centerCrop()Lcom/squareup/picasso/RequestCreator;

    move-result-object p3

    .line 146
    invoke-virtual {p3}, Lcom/squareup/picasso/RequestCreator;->onlyScaleDown()Lcom/squareup/picasso/RequestCreator;

    move-result-object p3

    new-instance p4, Lnet/gogame/chat/ChatAdapterViewFactory$1;

    invoke-direct {p4, p0, p6, p2}, Lnet/gogame/chat/ChatAdapterViewFactory$1;-><init>(Lnet/gogame/chat/ChatAdapterViewFactory;Ljava/lang/String;Landroid/widget/ImageView;)V

    .line 147
    invoke-virtual {p3, p2, p4}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;Lcom/squareup/picasso/Callback;)V

    .line 166
    new-instance p3, Lnet/gogame/chat/ChatAdapterViewFactory$2;

    invoke-direct {p3, p0, p6}, Lnet/gogame/chat/ChatAdapterViewFactory$2;-><init>(Lnet/gogame/chat/ChatAdapterViewFactory;Ljava/lang/String;)V

    invoke-virtual {p2, p3}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p1

    .line 130
    :cond_1
    :goto_0
    invoke-virtual {p0, p1, p2}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public getAgentMessageView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;
    .locals 0

    .line 112
    invoke-direct/range {p0 .. p5}, Lnet/gogame/chat/ChatAdapterViewFactory;->initAgentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    .line 115
    sget p2, Lcom/zopim/android/sdk/R$id;->agentTextView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    .line 116
    invoke-virtual {p2, p6}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 118
    sget p2, Lcom/zopim/android/sdk/R$id;->optionsLinearLayout:I

    .line 119
    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/LinearLayout;

    const/16 p3, 0x8

    .line 120
    invoke-virtual {p2, p3}, Landroid/widget/LinearLayout;->setVisibility(I)V

    return-object p1
.end method

.method public getAgentOptionsView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;)Landroid/view/View;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/view/View;",
            "Landroid/view/ViewGroup;",
            "Z",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lnet/gogame/chat/ChatAdapterViewFactory$Option;",
            ">;",
            "Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;",
            ")",
            "Landroid/view/View;"
        }
    .end annotation

    if-eqz p7, :cond_7

    .line 180
    invoke-interface {p7}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto/16 :goto_3

    .line 184
    :cond_0
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {v0}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    .line 185
    invoke-direct/range {p0 .. p5}, Lnet/gogame/chat/ChatAdapterViewFactory;->initAgentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;)Landroid/view/View;

    move-result-object p1

    .line 188
    sget p3, Lcom/zopim/android/sdk/R$id;->agentTextView:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/TextView;

    .line 189
    invoke-virtual {p3, p6}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 191
    sget p3, Lcom/zopim/android/sdk/R$id;->optionsLinearLayout:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/LinearLayout;

    const/4 p4, 0x0

    .line 193
    invoke-virtual {p3, p4}, Landroid/widget/LinearLayout;->setVisibility(I)V

    const/4 p5, 0x0

    .line 196
    invoke-interface {p7}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p6

    :cond_1
    invoke-interface {p6}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {p6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/chat/ChatAdapterViewFactory$Option;

    if-eqz v1, :cond_1

    .line 197
    invoke-interface {v1}, Lnet/gogame/chat/ChatAdapterViewFactory$Option;->isSelected()Z

    move-result v2

    if-eqz v2, :cond_1

    move-object p5, v1

    :cond_2
    if-eqz p5, :cond_4

    .line 203
    sget p6, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_option_sublayout:I

    invoke-virtual {v0, p6, p2, p4}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p2

    .line 205
    sget p4, Lcom/zopim/android/sdk/R$id;->chatOption:I

    invoke-virtual {p2, p4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p4

    check-cast p4, Landroid/widget/LinearLayout;

    .line 207
    sget p6, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p7, 0x10

    if-lt p6, p7, :cond_3

    .line 208
    sget p6, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rounded_corner_for_visitor:I

    invoke-direct {p0, p6}, Lnet/gogame/chat/ChatAdapterViewFactory;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p6

    invoke-virtual {p4, p6}, Landroid/widget/LinearLayout;->setBackground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    .line 211
    :cond_3
    sget p6, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rounded_corner_for_visitor:I

    invoke-direct {p0, p6}, Lnet/gogame/chat/ChatAdapterViewFactory;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p6

    invoke-virtual {p4, p6}, Landroid/widget/LinearLayout;->setBackgroundDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 215
    :goto_0
    sget p4, Lcom/zopim/android/sdk/R$id;->optionBulletImageView:I

    invoke-virtual {p2, p4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p4

    check-cast p4, Landroid/widget/ImageView;

    const/16 p6, 0x8

    .line 217
    invoke-virtual {p4, p6}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 219
    sget p4, Lcom/zopim/android/sdk/R$id;->optionLabelTextView:I

    invoke-virtual {p2, p4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p4

    check-cast p4, Landroid/widget/TextView;

    .line 221
    invoke-interface {p5}, Lnet/gogame/chat/ChatAdapterViewFactory$Option;->getLabel()Ljava/lang/String;

    move-result-object p5

    invoke-virtual {p4, p5}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 p5, -0x1

    .line 222
    invoke-virtual {p4, p5}, Landroid/widget/TextView;->setTextColor(I)V

    .line 224
    invoke-virtual {p3, p2}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    goto :goto_2

    .line 226
    :cond_4
    invoke-interface {p7}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p5

    :cond_5
    :goto_1
    invoke-interface {p5}, Ljava/util/Iterator;->hasNext()Z

    move-result p6

    if-eqz p6, :cond_6

    invoke-interface {p5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p6

    check-cast p6, Lnet/gogame/chat/ChatAdapterViewFactory$Option;

    if-eqz p6, :cond_5

    .line 228
    sget p7, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_option_sublayout:I

    invoke-virtual {v0, p7, p2, p4}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p7

    .line 230
    sget v1, Lcom/zopim/android/sdk/R$id;->optionLabelTextView:I

    invoke-virtual {p7, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    .line 232
    invoke-interface {p6}, Lnet/gogame/chat/ChatAdapterViewFactory$Option;->getLabel()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 233
    new-instance v2, Lnet/gogame/chat/ChatAdapterViewFactory$3;

    invoke-direct {v2, p0, p8, p6}, Lnet/gogame/chat/ChatAdapterViewFactory$3;-><init>(Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;Lnet/gogame/chat/ChatAdapterViewFactory$Option;)V

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 240
    invoke-virtual {p3, p7}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    goto :goto_1

    :cond_6
    :goto_2
    return-object p1

    .line 181
    :cond_7
    :goto_3
    invoke-virtual {p0, p1, p2}, Lnet/gogame/chat/ChatAdapterViewFactory;->getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public getEmptyView(Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 2

    const-string v0, "empty"

    .line 51
    invoke-direct {p0, p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 52
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p1}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p1

    .line 53
    sget v0, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_empty_layout:I

    const/4 v1, 0x0

    invoke-virtual {p1, v0, p2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    :cond_0
    return-object p1
.end method

.method public getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;
    .locals 1

    const/4 v0, 0x0

    .line 60
    invoke-virtual {p0, p1, p2, p3, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public getNotificationView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;Z)Landroid/view/View;
    .locals 2

    const-string v0, "notification"

    .line 65
    invoke-direct {p0, p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 66
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p1}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p1

    .line 67
    sget v0, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_notification_layout:I

    invoke-virtual {p1, v0, p2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 70
    :cond_0
    sget p2, Lcom/zopim/android/sdk/R$id;->textView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    .line 71
    invoke-virtual {p2, p3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    if-eqz p4, :cond_1

    const/16 p3, 0x8

    .line 73
    invoke-virtual {p2, p3}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_0

    .line 75
    :cond_1
    invoke-virtual {p2, v1}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_0
    return-object p1
.end method

.method public getRatingView(Landroid/view/View;Landroid/view/ViewGroup;Lnet/gogame/chat/ChatContext$Rating;Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;)Landroid/view/View;
    .locals 2

    const-string v0, "rating"

    .line 299
    invoke-direct {p0, p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 300
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p1}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p1

    .line 301
    sget v0, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_rating_layout:I

    const/4 v1, 0x0

    invoke-virtual {p1, v0, p2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 305
    :cond_0
    sget p2, Lcom/zopim/android/sdk/R$id;->rateGoodImageView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    .line 307
    sget v0, Lcom/zopim/android/sdk/R$id;->rateBadImageView:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    .line 309
    sget-object v1, Lnet/gogame/chat/ChatContext$Rating;->GOOD:Lnet/gogame/chat/ChatContext$Rating;

    if-ne p3, v1, :cond_1

    .line 310
    sget v1, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rate_good_selected:I

    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    .line 311
    sget v1, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rate_bad_unselected:I

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    goto :goto_0

    .line 312
    :cond_1
    sget-object v1, Lnet/gogame/chat/ChatContext$Rating;->BAD:Lnet/gogame/chat/ChatContext$Rating;

    if-ne p3, v1, :cond_2

    .line 313
    sget v1, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rate_good_unselected:I

    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    .line 314
    sget v1, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rate_bad_selected:I

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    goto :goto_0

    .line 316
    :cond_2
    sget v1, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rate_good_unselected:I

    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    .line 317
    sget v1, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_rate_bad_unselected:I

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    .line 319
    :goto_0
    new-instance v1, Lnet/gogame/chat/ChatAdapterViewFactory$5;

    invoke-direct {v1, p0, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory$5;-><init>(Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatContext$Rating;Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;)V

    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 336
    new-instance p2, Lnet/gogame/chat/ChatAdapterViewFactory$6;

    invoke-direct {p2, p0, p3, p4}, Lnet/gogame/chat/ChatAdapterViewFactory$6;-><init>(Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatContext$Rating;Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;)V

    invoke-virtual {v0, p2}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p1
.end method

.method public getVisitorAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;Landroid/net/Uri;)Landroid/view/View;
    .locals 2

    const-string v0, "visitorMessage"

    .line 265
    invoke-direct {p0, p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 266
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p1}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p1

    .line 267
    sget v0, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_visitor_layout:I

    invoke-virtual {p1, v0, p2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 271
    :cond_0
    sget p2, Lcom/zopim/android/sdk/R$id;->messageTextView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    const/16 v0, 0x8

    .line 272
    invoke-virtual {p2, v0}, Landroid/widget/TextView;->setVisibility(I)V

    const/4 v0, 0x0

    .line 273
    invoke-virtual {p2, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 275
    sget p2, Lcom/zopim/android/sdk/R$id;->attachmentThumbnailImageView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    .line 277
    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 278
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    const/4 v1, 0x1

    .line 279
    invoke-virtual {v0, v1}, Lcom/squareup/picasso/Picasso;->setLoggingEnabled(Z)V

    .line 281
    invoke-virtual {v0, p3}, Lcom/squareup/picasso/Picasso;->load(Landroid/net/Uri;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    const/16 v1, 0x190

    .line 282
    invoke-virtual {v0, v1, v1}, Lcom/squareup/picasso/RequestCreator;->resize(II)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    .line 283
    invoke-virtual {v0}, Lcom/squareup/picasso/RequestCreator;->onlyScaleDown()Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    .line 284
    invoke-virtual {v0}, Lcom/squareup/picasso/RequestCreator;->centerCrop()Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    .line 285
    invoke-virtual {v0, p2}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;)V

    .line 286
    new-instance v0, Lnet/gogame/chat/ChatAdapterViewFactory$4;

    invoke-direct {v0, p0, p3}, Lnet/gogame/chat/ChatAdapterViewFactory$4;-><init>(Lnet/gogame/chat/ChatAdapterViewFactory;Landroid/net/Uri;)V

    invoke-virtual {p2, v0}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object p1
.end method

.method public getVisitorMessageView(Landroid/view/View;Landroid/view/ViewGroup;Ljava/lang/String;)Landroid/view/View;
    .locals 2

    const-string v0, "visitorMessage"

    .line 249
    invoke-direct {p0, p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->viewIsOfType(Landroid/view/View;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 250
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory;->context:Landroid/content/Context;

    invoke-static {p1}, Lnet/gogame/chat/DisplayUtils;->getLayoutInflater(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p1

    .line 251
    sget v0, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_item_visitor_layout:I

    invoke-virtual {p1, v0, p2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 255
    :cond_0
    sget p2, Lcom/zopim/android/sdk/R$id;->attachmentThumbnailImageView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    const/16 v0, 0x8

    .line 257
    invoke-virtual {p2, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 258
    sget p2, Lcom/zopim/android/sdk/R$id;->messageTextView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    .line 259
    invoke-virtual {p2, v1}, Landroid/widget/TextView;->setVisibility(I)V

    .line 260
    invoke-virtual {p2, p3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-object p1
.end method
