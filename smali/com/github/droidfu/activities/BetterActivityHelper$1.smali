.class Lcom/github/droidfu/activities/BetterActivityHelper$1;
.super Ljava/lang/Object;
.source "BetterActivityHelper.java"

# interfaces
.implements Landroid/content/DialogInterface$OnKeyListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/github/droidfu/activities/BetterActivityHelper;->createProgressDialog(Landroid/app/Activity;II)Landroid/app/ProgressDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private final synthetic val$activity:Landroid/app/Activity;


# direct methods
.method constructor <init>(Landroid/app/Activity;)V
    .locals 0

    .line 1
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterActivityHelper$1;->val$activity:Landroid/app/Activity;

    .line 93
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onKey(Landroid/content/DialogInterface;ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 95
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterActivityHelper$1;->val$activity:Landroid/app/Activity;

    invoke-virtual {p1, p2, p3}, Landroid/app/Activity;->onKeyDown(ILandroid/view/KeyEvent;)Z

    const/4 p1, 0x0

    return p1
.end method
