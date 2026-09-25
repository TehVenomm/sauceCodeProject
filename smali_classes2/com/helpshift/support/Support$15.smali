.class Lcom/helpshift/support/Support$15;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->_setTheme(I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/Support;

.field final synthetic val$themeResourceId:I


# direct methods
.method constructor <init>(Lcom/helpshift/support/Support;I)V
    .locals 0

    .line 1514
    iput-object p1, p0, Lcom/helpshift/support/Support$15;->this$0:Lcom/helpshift/support/Support;

    iput p2, p0, Lcom/helpshift/support/Support$15;->val$themeResourceId:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 1517
    iget v0, p0, Lcom/helpshift/support/Support$15;->val$themeResourceId:I

    invoke-static {v0}, Lcom/helpshift/support/SupportInternal;->setTheme(I)V

    return-void
.end method
