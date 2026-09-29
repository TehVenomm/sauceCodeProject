.class final Lcom/zopim/android/sdk/chatlog/n;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;


# static fields
.field private static final a:Ljava/lang/String; = "n"


# instance fields
.field private b:Landroid/widget/TextView;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/view/View;)V
    .locals 1

    invoke-direct {p0, p1}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    sget v0, Lcom/zopim/android/sdk/R$id;->message_text:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/n;->b:Landroid/widget/TextView;

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 1

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/n;->a:Ljava/lang/String;

    const-string v0, "Item must not be null"

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/n;->b:Landroid/widget/TextView;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-void
.end method
