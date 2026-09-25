.class Lcom/github/droidfu/activities/BetterActivityHelper$5;
.super Ljava/lang/Object;
.source "BetterActivityHelper.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/github/droidfu/activities/BetterActivityHelper;->newListDialog(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;Lcom/github/droidfu/dialogs/DialogClickListener;ZI)Landroid/app/Dialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private final synthetic val$closeOnSelect:Z

.field private final synthetic val$elements:Ljava/util/List;

.field private final synthetic val$listener:Lcom/github/droidfu/dialogs/DialogClickListener;


# direct methods
.method constructor <init>(ZLcom/github/droidfu/dialogs/DialogClickListener;Ljava/util/List;)V
    .locals 0

    .line 1
    iput-boolean p1, p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;->val$closeOnSelect:Z

    iput-object p2, p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;->val$listener:Lcom/github/droidfu/dialogs/DialogClickListener;

    iput-object p3, p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;->val$elements:Ljava/util/List;

    .line 251
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 1

    .line 254
    iget-boolean v0, p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;->val$closeOnSelect:Z

    if-eqz v0, :cond_0

    .line 255
    invoke-interface {p1}, Landroid/content/DialogInterface;->dismiss()V

    .line 256
    :cond_0
    iget-object p1, p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;->val$listener:Lcom/github/droidfu/dialogs/DialogClickListener;

    iget-object v0, p0, Lcom/github/droidfu/activities/BetterActivityHelper$5;->val$elements:Ljava/util/List;

    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    invoke-interface {p1, p2, v0}, Lcom/github/droidfu/dialogs/DialogClickListener;->onClick(ILjava/lang/Object;)V

    return-void
.end method
