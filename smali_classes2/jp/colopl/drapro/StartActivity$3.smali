.class Ljp/colopl/drapro/StartActivity$3;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity;->inappbillingStart(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/StartActivity;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 567
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$3;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 570
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$3;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->checkInventory()V

    return-void
.end method
