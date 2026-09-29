.class Lcom/zopim/android/sdk/chatlog/i;
.super Landroidx/recyclerview/widget/RecyclerView$Adapter;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$Adapter<",
        "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;",
        ">;"
    }
.end annotation


# static fields
.field private static final a:Ljava/lang/String; = "i"

.field private static final b:I


# instance fields
.field private c:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/chatlog/aa;",
            ">;"
        }
    .end annotation
.end field

.field private d:Landroid/content/Context;

.field private e:Lcom/zopim/android/sdk/api/Chat;

.field private final f:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    invoke-static {}, Lcom/zopim/android/sdk/chatlog/aa$a;->values()[Lcom/zopim/android/sdk/chatlog/aa$a;

    move-result-object v0

    array-length v0, v0

    sput v0, Lcom/zopim/android/sdk/chatlog/i;->b:I

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$Adapter;-><init>()V

    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->f:Ljava/lang/Object;

    return-void
.end method

.method constructor <init>(Landroid/content/Context;Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/chatlog/aa;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$Adapter;-><init>()V

    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->f:Ljava/lang/Object;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/i;->d:Landroid/content/Context;

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/chatlog/i;)Lcom/zopim/android/sdk/api/Chat;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/i;->e:Lcom/zopim/android/sdk/api/Chat;

    return-object p0
.end method

.method private a(Landroid/view/View;Z)V
    .locals 3

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    const-string p2, "View must not be null. Skipping row item padding update."

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    if-eqz p2, :cond_1

    :try_start_0
    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/i;->d:Landroid/content/Context;

    invoke-virtual {p2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$dimen;->lead_message_padding_top:I

    invoke-virtual {p2, v0}, Landroid/content/res/Resources;->getDimension(I)F

    move-result p2

    invoke-virtual {p1}, Landroid/view/View;->getPaddingLeft()I

    move-result v0

    float-to-int p2, p2

    invoke-virtual {p1}, Landroid/view/View;->getPaddingRight()I

    move-result v1

    invoke-virtual {p1}, Landroid/view/View;->getPaddingBottom()I

    move-result v2

    :goto_0
    invoke-virtual {p1, v0, p2, v1, v2}, Landroid/view/View;->setPadding(IIII)V

    goto :goto_2

    :catch_0
    move-exception p1

    goto :goto_1

    :cond_1
    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/i;->d:Landroid/content/Context;

    invoke-virtual {p2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$dimen;->chat_message_padding_top:I

    invoke-virtual {p2, v0}, Landroid/content/res/Resources;->getDimension(I)F

    move-result p2

    invoke-virtual {p1}, Landroid/view/View;->getPaddingLeft()I

    move-result v0

    float-to-int p2, p2

    invoke-virtual {p1}, Landroid/view/View;->getPaddingRight()I

    move-result v1

    invoke-virtual {p1}, Landroid/view/View;->getPaddingBottom()I

    move-result v2
    :try_end_0
    .catch Landroid/content/res/Resources$NotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :goto_1
    sget-object p2, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    const-string v0, "Can not find padding dimension.Skipping."

    invoke-static {p2, v0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_2
    return-void
.end method

.method private a(Lcom/zopim/android/sdk/chatlog/aa$a;I)Z
    .locals 3

    add-int/lit8 v0, p2, -0x1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/chatlog/i;->getItemViewType(I)I

    move-result v1

    invoke-static {v1}, Lcom/zopim/android/sdk/chatlog/aa$a;->a(I)Lcom/zopim/android/sdk/chatlog/aa$a;

    move-result-object v1

    const/4 v2, 0x1

    if-ne p1, v1, :cond_0

    invoke-virtual {p0, p2}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object p1

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object p2

    iget-object p2, p2, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    if-eqz p1, :cond_0

    invoke-virtual {p1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x0

    return p1

    :cond_0
    return v2
.end method


# virtual methods
.method public a(I)V
    .locals 2

    :try_start_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemRemoved(I)V
    :try_end_0
    .catch Ljava/lang/UnsupportedOperationException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    sget-object v0, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    const-string v1, "Can not remove item. Item does not exist."

    goto :goto_0

    :catch_1
    move-exception p1

    sget-object v0, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    const-string v1, "Can not remove an item from the adapter."

    :goto_0
    invoke-static {v0, v1, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_1
    return-void
.end method

.method public a(Lcom/zopim/android/sdk/api/Chat;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/i;->e:Lcom/zopim/android/sdk/api/Chat;

    return-void
.end method

.method public a(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->f:Ljava/lang/Object;

    monitor-enter v0

    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method

.method public b(I)Lcom/zopim/android/sdk/chatlog/aa;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    if-eqz v0, :cond_1

    if-ltz p1, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/aa;

    return-object p1

    :cond_1
    :goto_0
    new-instance p1, Lcom/zopim/android/sdk/chatlog/aa;

    invoke-direct {p1}, Lcom/zopim/android/sdk/chatlog/aa;-><init>()V

    return-object p1
.end method

.method public getItemCount()I
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getItemViewType(I)I
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    if-eqz v0, :cond_1

    if-ltz p1, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/aa;

    if-nez p1, :cond_2

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/chatlog/aa$a;->a:Lcom/zopim/android/sdk/chatlog/aa$a;

    :goto_1
    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/aa$a;->a()I

    move-result p1

    return p1

    :cond_2
    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    goto :goto_1
.end method

.method public onBindViewHolder(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V
    .locals 7

    invoke-virtual {p0, p2}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object v0

    invoke-virtual {p0, p2}, Lcom/zopim/android/sdk/chatlog/i;->getItemViewType(I)I

    move-result v1

    invoke-static {v1}, Lcom/zopim/android/sdk/chatlog/aa$a;->a(I)Lcom/zopim/android/sdk/chatlog/aa$a;

    move-result-object v1

    sget-object v2, Lcom/zopim/android/sdk/chatlog/m;->a:[I

    invoke-virtual {v1}, Lcom/zopim/android/sdk/chatlog/aa$a;->ordinal()I

    move-result v1

    aget v1, v2, v1

    packed-switch v1, :pswitch_data_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    const-string p2, "Can not inflate unknown adapter item type. This may cause NullPointerException down the line."

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto/16 :goto_3

    :pswitch_0
    instance-of p2, p1, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    if-eqz p2, :cond_3

    instance-of p2, v0, Lcom/zopim/android/sdk/chatlog/t;

    if-eqz p2, :cond_3

    check-cast p1, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    check-cast v0, Lcom/zopim/android/sdk/chatlog/t;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->a(Lcom/zopim/android/sdk/chatlog/t;)V

    goto/16 :goto_3

    :pswitch_1
    instance-of p2, p1, Lcom/zopim/android/sdk/chatlog/n;

    if-eqz p2, :cond_3

    check-cast p1, Lcom/zopim/android/sdk/chatlog/n;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/chatlog/n;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    goto/16 :goto_3

    :pswitch_2
    instance-of p2, p1, Lcom/zopim/android/sdk/chatlog/h;

    if-eqz p2, :cond_3

    check-cast p1, Lcom/zopim/android/sdk/chatlog/h;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/chatlog/h;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    goto/16 :goto_3

    :pswitch_3
    instance-of v1, p1, Lcom/zopim/android/sdk/chatlog/f;

    if-eqz v1, :cond_3

    move-object v1, p1

    check-cast v1, Lcom/zopim/android/sdk/chatlog/f;

    check-cast v0, Lcom/zopim/android/sdk/chatlog/g;

    sget-object v2, Lcom/zopim/android/sdk/chatlog/aa$a;->c:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-direct {p0, v2, p2}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/aa$a;I)Z

    move-result p2

    invoke-virtual {v1, p2}, Lcom/zopim/android/sdk/chatlog/f;->a(Z)V

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/chatlog/f;->a(Lcom/zopim/android/sdk/chatlog/g;)V

    :goto_0
    iget-object p1, p1, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;->itemView:Landroid/view/View;

    invoke-direct {p0, p1, p2}, Lcom/zopim/android/sdk/chatlog/i;->a(Landroid/view/View;Z)V

    goto/16 :goto_3

    :pswitch_4
    instance-of v1, p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    if-eqz v1, :cond_3

    instance-of v1, v0, Lcom/zopim/android/sdk/chatlog/a;

    if-eqz v1, :cond_3

    check-cast p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    move-object v1, v0

    check-cast v1, Lcom/zopim/android/sdk/chatlog/a;

    sget-object v2, Lcom/zopim/android/sdk/chatlog/aa$a;->c:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-direct {p0, v2, p2}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/aa$a;I)Z

    move-result p2

    invoke-virtual {p1, p2}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a(Z)V

    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-direct {p0, v2, p2}, Lcom/zopim/android/sdk/chatlog/i;->a(Landroid/view/View;Z)V

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/i;->c:Ljava/util/List;

    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    const/4 v2, 0x0

    const/4 v3, 0x0

    :cond_0
    :goto_1
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/zopim/android/sdk/chatlog/aa;

    sget-object v5, Lcom/zopim/android/sdk/chatlog/aa$a;->c:Lcom/zopim/android/sdk/chatlog/aa$a;

    iget-object v6, v4, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    if-ne v5, v6, :cond_0

    iget-object v5, v4, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    iget-object v6, v0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    iget-object v4, v4, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    iget-object v5, v0, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    if-ne v4, v5, :cond_1

    const/4 v3, 0x1

    goto :goto_1

    :cond_1
    invoke-virtual {p1, v3}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b(Z)V

    iget-object p2, p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    invoke-virtual {p2}, Landroid/widget/LinearLayout;->removeAllViews()V

    iget-object p2, v1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    if-eqz p2, :cond_2

    iget-object p2, v1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length p2, p2

    if-lez p2, :cond_2

    sget-object p2, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    const-string v0, "Inflating agent questionnaire view"

    invoke-static {p2, v0}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/i;->d:Landroid/content/Context;

    const-string v0, "layout_inflater"

    invoke-virtual {p2, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Landroid/view/LayoutInflater;

    :goto_2
    iget-object v0, v1, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    array-length v0, v0

    if-ge v2, v0, :cond_2

    sget v0, Lcom/zopim/android/sdk/R$layout;->questinnaire_option:I

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a:Landroid/widget/LinearLayout;

    invoke-virtual {p2, v0, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    add-int/lit8 v2, v2, 0x1

    goto :goto_2

    :cond_2
    invoke-virtual {p1, v1}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a(Lcom/zopim/android/sdk/chatlog/a;)V

    goto :goto_3

    :pswitch_5
    instance-of v1, p1, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    if-eqz v1, :cond_3

    instance-of v1, v0, Lcom/zopim/android/sdk/chatlog/ab;

    if-eqz v1, :cond_3

    move-object v1, p1

    check-cast v1, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    check-cast v0, Lcom/zopim/android/sdk/chatlog/ab;

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->a(Lcom/zopim/android/sdk/chatlog/ab;)V

    sget-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->b:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-direct {p0, v0, p2}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/aa$a;I)Z

    move-result p2

    goto/16 :goto_0

    :cond_3
    :goto_3
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_5
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onCreateViewHolder(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
    .locals 2

    invoke-static {p2}, Lcom/zopim/android/sdk/chatlog/aa$a;->a(I)Lcom/zopim/android/sdk/chatlog/aa$a;

    move-result-object p2

    sget-object v0, Lcom/zopim/android/sdk/chatlog/m;->a:[I

    invoke-virtual {p2}, Lcom/zopim/android/sdk/chatlog/aa$a;->ordinal()I

    move-result v1

    aget v0, v0, v1

    const/4 v1, 0x0

    packed-switch v0, :pswitch_data_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/i;->a:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Can not inflate "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p2, " adapter item type. This may cause NullPointerException down the line."

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const/4 p1, 0x0

    return-object p1

    :pswitch_0
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_chat_rating:I

    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    new-instance p2, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/l;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/l;-><init>(Lcom/zopim/android/sdk/chatlog/i;)V

    invoke-direct {p2, p1, v0}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;-><init>(Landroid/view/View;Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;)V

    return-object p2

    :pswitch_1
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_member_event:I

    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    new-instance p2, Lcom/zopim/android/sdk/chatlog/n;

    invoke-direct {p2, p1}, Lcom/zopim/android/sdk/chatlog/n;-><init>(Landroid/view/View;)V

    return-object p2

    :pswitch_2
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_event_message:I

    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    new-instance p2, Lcom/zopim/android/sdk/chatlog/h;

    invoke-direct {p2, p1}, Lcom/zopim/android/sdk/chatlog/h;-><init>(Landroid/view/View;)V

    return-object p2

    :pswitch_3
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_agent_typing:I

    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    new-instance p2, Lcom/zopim/android/sdk/chatlog/f;

    invoke-direct {p2, p1}, Lcom/zopim/android/sdk/chatlog/f;-><init>(Landroid/view/View;)V

    return-object p2

    :pswitch_4
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_agent_message:I

    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    new-instance p2, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/k;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/k;-><init>(Lcom/zopim/android/sdk/chatlog/i;)V

    invoke-direct {p2, p1, v0}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;-><init>(Landroid/view/View;Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;)V

    return-object p2

    :pswitch_5
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_visitor_message:I

    invoke-virtual {p2, v0, p1, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    new-instance p2, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/j;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/j;-><init>(Lcom/zopim/android/sdk/chatlog/i;)V

    invoke-direct {p2, p1, v0}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;-><init>(Landroid/view/View;Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;)V

    return-object p2

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_5
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
