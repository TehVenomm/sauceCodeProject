.class final Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;"
    }
.end annotation


# static fields
.field private static final e:Ljava/lang/String; = "ChatRatingHolder"


# instance fields
.field a:Landroid/view/View$OnClickListener;

.field b:Landroid/view/View$OnClickListener;

.field c:Landroid/view/View$OnClickListener;

.field d:Landroid/view/View$OnClickListener;

.field private f:Landroid/widget/RadioGroup;

.field private g:Landroid/widget/RadioButton;

.field private h:Landroid/widget/RadioButton;

.field private i:Landroid/view/View;

.field private j:Landroid/view/View;

.field private k:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

.field private l:Landroid/widget/TextView;

.field private m:Lcom/zopim/android/sdk/chatlog/t;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/view/View;Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;)V
    .locals 1

    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/o;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/o;-><init>(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->a:Landroid/view/View$OnClickListener;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/p;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/p;-><init>(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->b:Landroid/view/View$OnClickListener;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/q;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/q;-><init>(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->c:Landroid/view/View$OnClickListener;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/r;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/r;-><init>(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->d:Landroid/view/View$OnClickListener;

    sget v0, Lcom/zopim/android/sdk/R$id;->rating_button_group:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/RadioGroup;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->f:Landroid/widget/RadioGroup;

    sget v0, Lcom/zopim/android/sdk/R$id;->positive_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/RadioButton;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->g:Landroid/widget/RadioButton;

    sget v0, Lcom/zopim/android/sdk/R$id;->negative_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/RadioButton;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->h:Landroid/widget/RadioButton;

    sget v0, Lcom/zopim/android/sdk/R$id;->add_comment_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->i:Landroid/view/View;

    sget v0, Lcom/zopim/android/sdk/R$id;->edit_comment_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->j:Landroid/view/View;

    sget v0, Lcom/zopim/android/sdk/R$id;->comment_message:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->l:Landroid/widget/TextView;

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->g:Landroid/widget/RadioButton;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->a:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, v0}, Landroid/widget/RadioButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->h:Landroid/widget/RadioButton;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->b:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, v0}, Landroid/widget/RadioButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->i:Landroid/view/View;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->c:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->j:Landroid/view/View;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->d:Landroid/view/View$OnClickListener;

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->k:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/t;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->m:Lcom/zopim/android/sdk/chatlog/t;

    return-object p0
.end method

.method static synthetic b(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Landroid/widget/RadioGroup;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->f:Landroid/widget/RadioGroup;

    return-object p0
.end method

.method static synthetic c(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->k:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

    return-object p0
.end method

.method static synthetic d(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Landroid/widget/TextView;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->l:Landroid/widget/TextView;

    return-object p0
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/t;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->e:Ljava/lang/String;

    const-string v0, "Item must not be null"

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->m:Lcom/zopim/android/sdk/chatlog/t;

    sget-object v0, Lcom/zopim/android/sdk/chatlog/s;->a:[I

    iget-object v1, p1, Lcom/zopim/android/sdk/chatlog/t;->a:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->ordinal()I

    move-result v1

    aget v0, v0, v1

    const/4 v1, 0x1

    const/4 v2, 0x0

    packed-switch v0, :pswitch_data_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->g:Landroid/widget/RadioButton;

    invoke-virtual {v0, v2}, Landroid/widget/RadioButton;->setChecked(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->h:Landroid/widget/RadioButton;

    invoke-virtual {v0, v2}, Landroid/widget/RadioButton;->setChecked(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->i:Landroid/view/View;

    const/4 v3, 0x4

    invoke-virtual {v0, v3}, Landroid/view/View;->setVisibility(I)V

    goto :goto_1

    :pswitch_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->g:Landroid/widget/RadioButton;

    invoke-virtual {v0, v2}, Landroid/widget/RadioButton;->setChecked(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->h:Landroid/widget/RadioButton;

    invoke-virtual {v0, v1}, Landroid/widget/RadioButton;->setChecked(Z)V

    goto :goto_0

    :pswitch_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->g:Landroid/widget/RadioButton;

    invoke-virtual {v0, v1}, Landroid/widget/RadioButton;->setChecked(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->h:Landroid/widget/RadioButton;

    invoke-virtual {v0, v2}, Landroid/widget/RadioButton;->setChecked(Z)V

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->i:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    :goto_1
    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/t;->b:Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/t;->b:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_1

    goto :goto_2

    :cond_1
    const/4 v1, 0x0

    :goto_2
    const/16 v0, 0x8

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->i:Landroid/view/View;

    invoke-virtual {v1, v0}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->j:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->l:Landroid/widget/TextView;

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->l:Landroid/widget/TextView;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/t;->b:Ljava/lang/String;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_3

    :cond_2
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->j:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->l:Landroid/widget/TextView;

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_3
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
