.class final Lcom/helpshift/support/HSSearch$1;
.super Ljava/lang/Object;
.source "HSSearch.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/HSSearch;->init()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 43
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 0

    .line 45
    invoke-static {}, Lcom/helpshift/support/util/HSTransliterator;->init()V

    return-void
.end method
