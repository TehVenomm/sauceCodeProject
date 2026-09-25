.class final Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;"
    }
.end annotation


# static fields
.field private static final c:Ljava/lang/String; = "AgentMessageHolder"


# instance fields
.field public a:Landroid/widget/LinearLayout;

.field b:Landroid/view/View$OnClickListener;

.field private d:Landroid/widget/ImageView;

.field private e:Landroid/widget/TextView;

.field private f:Landroid/widget/TextView;

.field private g:Landroid/view/View;

.field private h:Landroid/widget/TextView;

.field private i:Landroid/widget/TextView;

.field private j:Landroid/widget/ImageView;

.field private k:Landroid/view/View;

.field private l:Landroid/widget/ImageView;

.field private m:Landroid/widget/ProgressBar;

.field private n:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;

.field private o:Landroid/content/Intent;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/view/View;Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;)V
    .locals 2

    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/d;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/d;-><init>(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b:Landroid/view/View$OnClickListener;

    sget v0, Lcom/zopim/android/sdk/R$id;->avatar_icon:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->d:Landroid/widget/ImageView;

    sget v0, Lcom/zopim/android/sdk/R$id;->message_text:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->f:Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$id;->agent_name:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->e:Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$id;->options_container:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/LinearLayout;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_document:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->g:Landroid/view/View;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_name:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->h:Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_size:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->i:Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_icon:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->j:Landroid/widget/ImageView;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_image_container:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->k:Landroid/view/View;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_thumbnail:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->l:Landroid/widget/ImageView;

    sget v0, Lcom/zopim/android/sdk/R$id;->attachment_progress:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ProgressBar;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->m:Landroid/widget/ProgressBar;

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    const v0, 0x40000001    # 2.0000002f

    invoke-virtual {p1, v0}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->k:Landroid/view/View;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->g:Landroid/view/View;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->n:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Landroid/widget/ProgressBar;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->m:Landroid/widget/ProgressBar;

    return-object p0
.end method

.method static synthetic a()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c:Ljava/lang/String;

    return-object v0
.end method

.method private a(JZ)Ljava/lang/String;
    .locals 9

    if-eqz p3, :cond_0

    const/16 v0, 0x3e8

    goto :goto_0

    :cond_0
    const/16 v0, 0x400

    :goto_0
    int-to-long v1, v0

    cmp-long v3, p1, v1

    if-gez v3, :cond_1

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p3, p1, p2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string p1, " B"

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_1
    long-to-double p1, p1

    invoke-static {p1, p2}, Ljava/lang/Math;->log(D)D

    move-result-wide v1

    int-to-double v3, v0

    invoke-static {v3, v4}, Ljava/lang/Math;->log(D)D

    move-result-wide v5

    div-double/2addr v1, v5

    double-to-int v0, v1

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    if-eqz p3, :cond_2

    const-string v2, "kMGTPE"

    goto :goto_1

    :cond_2
    const-string v2, "KMGTPE"

    :goto_1
    add-int/lit8 v5, v0, -0x1

    invoke-virtual {v2, v5}, Ljava/lang/String;->charAt(I)C

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    if-eqz p3, :cond_3

    const-string p3, ""

    goto :goto_2

    :cond_3
    const-string p3, "i"

    :goto_2
    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    sget-object v1, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "%.1f %sB"

    const/4 v5, 0x2

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    int-to-double v7, v0

    invoke-static {v3, v4, v7, v8}, Ljava/lang/Math;->pow(DD)D

    move-result-wide v3

    invoke-static {p1, p2}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr p1, v3

    invoke-static {p1, p2}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    aput-object p1, v5, v6

    const/4 p1, 0x1

    aput-object p3, v5, p1

    invoke-static {v1, v2, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method static synthetic b(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->n:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;

    return-object p0
.end method

.method private b(Lcom/zopim/android/sdk/chatlog/a;)V
    .locals 6

    if-eqz p1, :cond_4

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    if-eqz v0, :cond_4

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    invoke-virtual {v0}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Landroid/webkit/MimeTypeMap;->getFileExtensionFromUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    invoke-virtual {v1}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v2

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->getProviderUri(Landroid/content/Context;Ljava/io/File;)Landroid/net/Uri;

    move-result-object v2

    sget-object v3, Lcom/zopim/android/sdk/chatlog/e;->a:[I

    invoke-static {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->valueOfExtension(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/FileExtension;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->ordinal()I

    move-result v0

    aget v0, v3, v0

    const/4 v3, 0x1

    const/4 v4, 0x0

    const/16 v5, 0x8

    packed-switch v0, :pswitch_data_0

    goto/16 :goto_5

    :pswitch_0
    if-eqz v2, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    const-string v0, "image/*"

    invoke-virtual {p1, v2, v0}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    goto :goto_0

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    const-string v0, "image/*"

    invoke-virtual {p1, v1, v0}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    :goto_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->m:Landroid/widget/ProgressBar;

    invoke-virtual {p1, v4}, Landroid/widget/ProgressBar;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object p1

    invoke-virtual {p1, v1}, Lcom/squareup/picasso/Picasso;->load(Landroid/net/Uri;)Lcom/squareup/picasso/RequestCreator;

    move-result-object p1

    sget v0, Lcom/zopim/android/sdk/R$drawable;->bg_picasso_placeholder:I

    invoke-virtual {p1, v0}, Lcom/squareup/picasso/RequestCreator;->placeholder(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object p1

    sget v0, Lcom/zopim/android/sdk/R$drawable;->ic_chat_default_avatar:I

    invoke-virtual {p1, v0}, Lcom/squareup/picasso/RequestCreator;->error(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object p1

    new-instance v0, Lcom/zopim/android/sdk/util/CropSquareTransform;

    invoke-direct {v0}, Lcom/zopim/android/sdk/util/CropSquareTransform;-><init>()V

    invoke-virtual {p1, v0}, Lcom/squareup/picasso/RequestCreator;->transform(Lcom/squareup/picasso/Transformation;)Lcom/squareup/picasso/RequestCreator;

    move-result-object p1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->l:Landroid/widget/ImageView;

    new-instance v1, Lcom/zopim/android/sdk/chatlog/b;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/chatlog/b;-><init>(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)V

    invoke-virtual {p1, v0, v1}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;Lcom/squareup/picasso/Callback;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->g:Landroid/view/View;

    invoke-virtual {p1, v5}, Landroid/view/View;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->f:Landroid/widget/TextView;

    invoke-virtual {p1, v5}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->k:Landroid/view/View;

    invoke-virtual {p1, v4}, Landroid/view/View;->setVisibility(I)V

    goto :goto_5

    :pswitch_1
    if-eqz v2, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    const-string v1, "text/plain"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    goto :goto_1

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    :goto_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->j:Landroid/widget/ImageView;

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_attachment_txt:I

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->h:Landroid/widget/TextView;

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    if-eqz v2, :cond_3

    goto :goto_3

    :pswitch_2
    if-eqz v2, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    const-string v1, "application/pdf"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    goto :goto_2

    :cond_2
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    :goto_2
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->j:Landroid/widget/ImageView;

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_attachment_pdf:I

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->h:Landroid/widget/TextView;

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    if-eqz v0, :cond_3

    :goto_3
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->i:Landroid/widget/TextView;

    invoke-virtual {v0, v4}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->i:Landroid/widget/TextView;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-direct {p0, v1, v2, v3}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a(JZ)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_4

    :cond_3
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->i:Landroid/widget/TextView;

    invoke-virtual {p1, v5}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_4
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->g:Landroid/view/View;

    invoke-virtual {p1, v4}, Landroid/view/View;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->f:Landroid/widget/TextView;

    invoke-virtual {p1, v5}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->k:Landroid/view/View;

    invoke-virtual {p1, v5}, Landroid/view/View;->setVisibility(I)V

    :cond_4
    :goto_5
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method static synthetic c(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Landroid/content/Intent;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->o:Landroid/content/Intent;

    return-object p0
.end method

.method private c(Lcom/zopim/android/sdk/chatlog/a;)V
    .locals 3

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length v0, v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    invoke-virtual {v1}, Landroid/widget/LinearLayout;->getChildCount()I

    move-result v1

    if-eq v0, v1, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length v2, v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, " item options,  "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    invoke-virtual {v2}, Landroid/widget/LinearLayout;->getChildCount()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, " views."

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->w(Ljava/lang/String;Ljava/lang/String;)V

    sget-object v0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c:Ljava/lang/String;

    const-string v1, "Unexpected agent options length. Ignoring to avoid array index out bounds exception."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length v0, v0

    const/4 v1, 0x0

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    aget-object p1, p1, v1

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    sget p1, Lcom/zopim/android/sdk/R$drawable;->bg_chat_bubble_visitor:I

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setBackgroundResource(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    sget v2, Lcom/zopim/android/sdk/R$style;->chat_bubble_visitor:I

    invoke-virtual {v0, p1, v2}, Landroid/widget/TextView;->setTextAppearance(Landroid/content/Context;I)V

    invoke-virtual {v0, v1, v1, v1, v1}, Landroid/widget/TextView;->setCompoundDrawablesWithIntrinsicBounds(IIII)V

    goto :goto_1

    :goto_0
    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length v0, v0

    if-ge v1, v0, :cond_1

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    aget-object v0, v0, v1

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    invoke-virtual {v2, v1}, Landroid/widget/LinearLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroid/widget/TextView;

    invoke-virtual {v2, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const/4 v0, 0x1

    invoke-virtual {v2, v0}, Landroid/widget/TextView;->setClickable(Z)V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/c;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/c;-><init>(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)V

    invoke-virtual {v2, v0}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    :goto_1
    :pswitch_1
    return-void

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/a;)V
    .locals 3

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c:Ljava/lang/String;

    const-string v0, "Item must not be null"

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->e:Landroid/widget/TextView;

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->j:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    if-eqz v0, :cond_2

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_1

    goto :goto_0

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

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

    goto :goto_1

    :cond_2
    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$drawable;->ic_chat_default_avatar:I

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/Picasso;->load(I)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/util/CircleTransform;

    invoke-direct {v1}, Lcom/zopim/android/sdk/util/CircleTransform;-><init>()V

    :goto_1
    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->transform(Lcom/squareup/picasso/Transformation;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->d:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    const/4 v1, 0x0

    if-eqz v0, :cond_3

    const/4 v0, 0x1

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    :goto_2
    if-eqz v0, :cond_4

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b(Lcom/zopim/android/sdk/chatlog/a;)V

    goto :goto_3

    :cond_4
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->f:Landroid/widget/TextView;

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/a;->i:Ljava/lang/String;

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->k:Landroid/view/View;

    const/16 v2, 0x8

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->g:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->f:Landroid/widget/TextView;

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length v0, v0

    if-lez v0, :cond_5

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c(Lcom/zopim/android/sdk/chatlog/a;)V

    :cond_5
    :goto_3
    return-void
.end method

.method public a(Z)V
    .locals 1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->d:Landroid/widget/ImageView;

    const/4 v0, 0x0

    :goto_0
    invoke-virtual {p1, v0}, Landroid/widget/ImageView;->setVisibility(I)V

    goto :goto_1

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->d:Landroid/widget/ImageView;

    const/4 v0, 0x4

    goto :goto_0

    :goto_1
    return-void
.end method

.method public b(Z)V
    .locals 1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->e:Landroid/widget/TextView;

    const/4 v0, 0x0

    :goto_0
    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_1

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->e:Landroid/widget/TextView;

    const/16 v0, 0x8

    goto :goto_0

    :goto_1
    return-void
.end method
