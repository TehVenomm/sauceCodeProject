.class Ljp/colopl/drapro/AppHelper$1$2;
.super Ljava/lang/Object;
.source "AppHelper.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


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

    .line 121
    iput-object p1, p0, Ljp/colopl/drapro/AppHelper$1$2;->this$0:Ljp/colopl/drapro/AppHelper$1;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    const-string p1, "Cancel"

    const/4 p2, 0x0

    .line 124
    invoke-static {p2, p1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x0

    .line 125
    sput-boolean p1, Ljp/colopl/drapro/AppHelper;->isQuitDialogOpened:Z

    return-void
.end method
