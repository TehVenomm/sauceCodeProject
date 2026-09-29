.class final Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;"
    }
.end annotation


# static fields
.field private static final k:Ljava/lang/String; = "VisitorMessageHolder"


# instance fields
.field public a:Landroid/view/View;

.field public b:Landroid/widget/TextView;

.field public c:Landroid/widget/ImageView;

.field public d:Landroid/widget/TextView;

.field public e:Landroid/view/View;

.field public f:Landroid/widget/ImageView;

.field public g:Landroidx/core/widget/ContentLoadingProgressBar;

.field public h:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;

.field public i:Landroid/content/Intent;

.field j:Landroid/view/View$OnClickListener;

.field private l:Lcom/zopim/android/sdk/chatlog/ab;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/view/View;Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;)V
    .locals 2

    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->i:Landroid/content/Intent;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ae;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/ae;-><init>(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->j:Landroid/view/View$OnClickListener;

    sget v0, Lcom/zopim/android/sdk/R$id;->message_container:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->a:Landroid/view/View;

    sget v0, Lcom/zopim/android/sdk/R$id;->message_text:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->b:Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$id;->send_failed_label:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->d:Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$id;->send_failed_icon:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->c:Landroid/widget/ImageView;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_image_container:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->e:Landroid/view/View;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_thumbnail:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->f:Landroid/widget/ImageView;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_progress:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroidx/core/widget/ContentLoadingProgressBar;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->g:Landroidx/core/widget/ContentLoadingProgressBar;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->h:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->f:Landroid/widget/ImageView;

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->j:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, p2}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->i:Landroid/content/Intent;

    const/high16 p2, 0x40000000    # 2.0f

    invoke-virtual {p1, p2}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    return-void
.end method

.method static synthetic a()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->k:Ljava/lang/String;

    return-object v0
.end method

.method private a(I)V
    .locals 2

    const/16 v0, 0x64

    const/4 v1, 0x4

    if-eq p1, v0, :cond_0

    packed-switch p1, :pswitch_data_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->g:Landroidx/core/widget/ContentLoadingProgressBar;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroidx/core/widget/ContentLoadingProgressBar;->setVisibility(I)V

    goto :goto_0

    :cond_0
    :pswitch_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->g:Landroidx/core/widget/ContentLoadingProgressBar;

    invoke-virtual {p1, v1}, Landroidx/core/widget/ContentLoadingProgressBar;->setVisibility(I)V

    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch -0x1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method static synthetic a(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;I)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->a(I)V

    return-void
.end method

.method private b(Lcom/zopim/android/sdk/chatlog/ab;)V
    .locals 3

    if-eqz p1, :cond_0

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/chatlog/af;->a:[I

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-static {v1}, Lcom/zopim/android/sdk/attachment/FileExtension;->getExtension(Ljava/io/File;)Lcom/zopim/android/sdk/attachment/FileExtension;

    move-result-object v1

    invoke-virtual {v1}, Lcom/zopim/android/sdk/attachment/FileExtension;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/Picasso;->load(Ljava/io/File;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_default_avatar:I

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->error(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$drawable;->bg_picasso_placeholder:I

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->placeholder(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/util/CropSquareTransform;

    invoke-direct {v1}, Lcom/zopim/android/sdk/util/CropSquareTransform;-><init>()V

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->transform(Lcom/squareup/picasso/Transformation;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->f:Landroid/widget/ImageView;

    new-instance v2, Lcom/zopim/android/sdk/chatlog/ad;

    invoke-direct {v2, p0, p1}, Lcom/zopim/android/sdk/chatlog/ad;-><init>(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;Lcom/zopim/android/sdk/chatlog/ab;)V

    invoke-virtual {v0, v1, v2}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;Lcom/squareup/picasso/Callback;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->b:Landroid/widget/TextView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->e:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-static {p1}, Landroid/net/Uri;->fromFile(Ljava/io/File;)Landroid/net/Uri;

    move-result-object p1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->i:Landroid/content/Intent;

    const-string v1, "image/*"

    invoke-virtual {v0, p1, v1}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    :cond_0
    :goto_0
    :pswitch_1
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_1
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/ab;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->k:Ljava/lang/String;

    const-string v0, "Item must not be null"

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->l:Lcom/zopim/android/sdk/chatlog/ab;

    iget-boolean v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    const/16 v1, 0x8

    const/4 v2, 0x0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->c:Landroid/widget/ImageView;

    invoke-virtual {v0, v2}, Landroid/widget/ImageView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->d:Landroid/widget/TextView;

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->itemView:Landroid/view/View;

    new-instance v3, Lcom/zopim/android/sdk/chatlog/ac;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/chatlog/ac;-><init>(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;)V

    :goto_0
    invoke-virtual {v0, v3}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_1

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->d:Landroid/widget/TextView;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->c:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->itemView:Landroid/view/View;

    const/4 v3, 0x0

    goto :goto_0

    :goto_1
    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    if-eqz v0, :cond_2

    const/4 v0, 0x1

    goto :goto_2

    :cond_2
    const/4 v0, 0x0

    :goto_2
    if-eqz v0, :cond_3

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->b(Lcom/zopim/android/sdk/chatlog/ab;)V

    goto :goto_3

    :cond_3
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->b:Landroid/widget/TextView;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/ab;->i:Ljava/lang/String;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->b:Landroid/widget/TextView;

    invoke-virtual {p1, v2}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->e:Landroid/view/View;

    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    :goto_3
    return-void
.end method
