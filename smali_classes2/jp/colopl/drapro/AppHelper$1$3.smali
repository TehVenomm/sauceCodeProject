.class Ljp/colopl/drapro/AppHelper$1$3;
.super Ljava/lang/Object;
.source "AppHelper.java"

# interfaces
.implements Landroid/content/DialogInterface$OnCancelListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/AppHelper$1;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/AppHelper$1;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/AppHelper$1;)V
    .locals 0

    .line 129
    iput-object p1, p0, Ljp/colopl/drapro/AppHelper$1$3;->this$0:Ljp/colopl/drapro/AppHelper$1;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onCancel(Landroid/content/DialogInterface;)V
    .locals 1

    const-string p1, "Back key Cancel"

    const/4 v0, 0x0

    .line 132
    invoke-static {v0, p1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x0

    .line 133
    sput-boolean p1, Ljp/colopl/drapro/AppHelper;->isQuitDialogOpened:Z

    return-void
.end method
