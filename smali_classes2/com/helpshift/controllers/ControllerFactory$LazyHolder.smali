.class Lcom/helpshift/controllers/ControllerFactory$LazyHolder;
.super Ljava/lang/Object;
.source "ControllerFactory.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/controllers/ControllerFactory;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "LazyHolder"
.end annotation


# static fields
.field static final INSTANCE:Lcom/helpshift/controllers/ControllerFactory;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 27
    new-instance v0, Lcom/helpshift/controllers/ControllerFactory;

    invoke-direct {v0}, Lcom/helpshift/controllers/ControllerFactory;-><init>()V

    sput-object v0, Lcom/helpshift/controllers/ControllerFactory$LazyHolder;->INSTANCE:Lcom/helpshift/controllers/ControllerFactory;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
