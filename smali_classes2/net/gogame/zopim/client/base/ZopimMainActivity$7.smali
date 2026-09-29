.class Lnet/gogame/zopim/client/base/ZopimMainActivity$7;
.super Ljava/lang/Object;
.source "ZopimMainActivity.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/zopim/client/base/ZopimMainActivity;->createExitDialog()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V
    .locals 0

    .line 195
    iput-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$7;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    .line 199
    iget-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$7;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-virtual {p1}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->finish()V

    return-void
.end method
