.class public final Lnet/gogame/gopay/sdk/support/b;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnLongClickListener;


# instance fields
.field final synthetic a:Ljava/lang/String;

.field final synthetic b:Landroid/content/Context;


# direct methods
.method public constructor <init>(Ljava/lang/String;Landroid/content/Context;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/b;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/support/b;->b:Landroid/content/Context;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onLongClick(Landroid/view/View;)Z
    .locals 3

    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v0, 0x0

    const/16 v1, 0xb

    if-lt p1, v1, :cond_0

    const-string p1, "Build info"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/b;->a:Ljava/lang/String;

    invoke-static {p1, v1}, Landroid/content/ClipData;->newPlainText(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Landroid/content/ClipData;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/b;->b:Landroid/content/Context;

    const-string v2, "clipboard"

    invoke-virtual {v1, v2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/content/ClipboardManager;

    invoke-virtual {v1, p1}, Landroid/content/ClipboardManager;->setPrimaryClip(Landroid/content/ClipData;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/b;->b:Landroid/content/Context;

    const-string v1, "Build info copied to clipboard"

    invoke-static {p1, v1, v0}, Landroid/widget/Toast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    :cond_0
    return v0
.end method
