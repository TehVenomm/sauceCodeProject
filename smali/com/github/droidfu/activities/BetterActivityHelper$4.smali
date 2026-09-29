.class Lcom/github/droidfu/activities/BetterActivityHelper$4;
.super Ljava/lang/Object;
.source "BetterActivityHelper.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/github/droidfu/activities/BetterActivityHelper;->newErrorHandlerDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/Exception;)Landroid/app/AlertDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private final synthetic val$activity:Landroid/app/Activity;

.field private final synthetic val$intent:Landroid/content/Intent;


# direct methods
.method constructor <init>(Landroid/app/Activity;Landroid/content/Intent;)V
    .locals 0

    .line 1
    iput-object p1, p0, Lcom/github/droidfu/activities/BetterActivityHelper$4;->val$activity:Landroid/app/Activity;

    iput-object p2, p0, Lcom/github/droidfu/activities/BetterActivityHelper$4;->val$intent:Landroid/content/Intent;

    .line 202
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    .line 205
    invoke-interface {p1}, Landroid/content/DialogInterface;->dismiss()V

    .line 206
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterActivityHelper$4;->val$activity:Landroid/app/Activity;

    iget-object p2, p0, Lcom/github/droidfu/activities/BetterActivityHelper$4;->val$intent:Landroid/content/Intent;

    invoke-virtual {p1, p2}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method
