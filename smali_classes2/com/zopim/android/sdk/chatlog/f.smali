.class final Lcom/zopim/android/sdk/chatlog/f;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;"
    }
.end annotation


# static fields
.field private static final a:Ljava/lang/String;


# instance fields
.field private b:Landroid/widget/ImageView;

.field private c:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

.field private d:Z


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/chatlog/f;->a:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Landroid/view/View;)V
    .locals 1

    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/f;->d:Z

    sget v0, Lcom/zopim/android/sdk/R$id;->avatar_icon:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->b:Landroid/widget/ImageView;

    sget v0, Lcom/zopim/android/sdk/R$id;->typing_indicator:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/f;->c:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    return-void
.end method

.method private a()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0x10
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->c:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setVisibility(I)V

    iget-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/f;->d:Z

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->b:Landroid/widget/ImageView;

    :goto_0
    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    goto :goto_1

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->b:Landroid/widget/ImageView;

    const/4 v1, 0x4

    goto :goto_0

    :goto_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->c:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->start()V

    return-void
.end method

.method private b()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->c:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->stop()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->c:Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->b:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/g;)V
    .locals 2

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/f;->a:Ljava/lang/String;

    const-string v0, "Item must not be null"

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/Picasso;->load(Ljava/lang/String;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_default_avatar:I

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->error(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_default_avatar:I

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->placeholder(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/util/CircleTransform;

    invoke-direct {v1}, Lcom/zopim/android/sdk/util/CircleTransform;-><init>()V

    :goto_0
    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->transform(Lcom/squareup/picasso/Transformation;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/f;->b:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;)V

    goto :goto_1

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/f;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_default_avatar:I

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/Picasso;->load(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/util/CircleTransform;

    invoke-direct {v1}, Lcom/zopim/android/sdk/util/CircleTransform;-><init>()V

    goto :goto_0

    :goto_1
    iget-boolean p1, p1, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    if-eqz p1, :cond_2

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/f;->a()V

    goto :goto_2

    :cond_2
    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/f;->b()V

    :goto_2
    return-void
.end method

.method public a(Z)V
    .locals 0

    iput-boolean p1, p0, Lcom/zopim/android/sdk/chatlog/f;->d:Z

    return-void
.end method
