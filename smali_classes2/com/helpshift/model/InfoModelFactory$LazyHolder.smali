.class final Lcom/helpshift/model/InfoModelFactory$LazyHolder;
.super Ljava/lang/Object;
.source "InfoModelFactory.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/model/InfoModelFactory;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/model/InfoModelFactory;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 27
    new-instance v0, Lcom/helpshift/model/InfoModelFactory;

    invoke-direct {v0}, Lcom/helpshift/model/InfoModelFactory;-><init>()V

    sput-object v0, Lcom/helpshift/model/InfoModelFactory$LazyHolder;->INSTANCE:Lcom/helpshift/model/InfoModelFactory;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
