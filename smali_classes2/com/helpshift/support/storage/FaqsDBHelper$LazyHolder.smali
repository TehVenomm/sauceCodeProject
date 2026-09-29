.class Lcom/helpshift/support/storage/FaqsDBHelper$LazyHolder;
.super Ljava/lang/Object;
.source "FaqsDBHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/storage/FaqsDBHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/support/storage/FaqsDBHelper;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 81
    new-instance v0, Lcom/helpshift/support/storage/FaqsDBHelper;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/support/storage/FaqsDBHelper;-><init>(Landroid/content/Context;)V

    sput-object v0, Lcom/helpshift/support/storage/FaqsDBHelper$LazyHolder;->INSTANCE:Lcom/helpshift/support/storage/FaqsDBHelper;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 80
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
