.class public Lcom/helpshift/support/FaqTagFilter$Operator;
.super Ljava/lang/Object;
.source "FaqTagFilter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/FaqTagFilter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Operator"
.end annotation


# static fields
.field public static final AND:Ljava/lang/String; = "and"

.field public static final NOT:Ljava/lang/String; = "not"

.field public static final OR:Ljava/lang/String; = "or"

.field public static final UNDEFINED:Ljava/lang/String; = "undefined"

.field public static final valueSet:Ljava/util/HashSet;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashSet<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 79
    invoke-static {}, Lcom/helpshift/support/FaqTagFilter$Operator;->getSupportedValueSet()Ljava/util/HashSet;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/FaqTagFilter$Operator;->valueSet:Ljava/util/HashSet;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 73
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static getSupportedValueSet()Ljava/util/HashSet;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashSet<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 86
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    const-string v1, "and"

    .line 87
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "or"

    .line 88
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    const-string v1, "not"

    .line 89
    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    return-object v0
.end method
