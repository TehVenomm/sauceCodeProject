.class public Lcom/helpshift/support/SupportInternal$EnableContactUs;
.super Ljava/lang/Object;
.source "SupportInternal.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/SupportInternal;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "EnableContactUs"
.end annotation


# static fields
.field public static final AFTER_MARKING_ANSWER_UNHELPFUL:Ljava/lang/Integer;

.field public static final AFTER_VIEWING_FAQS:Ljava/lang/Integer;

.field public static final ALWAYS:Ljava/lang/Integer;

.field public static final NEVER:Ljava/lang/Integer;

.field public static final valueSet:Ljava/util/HashSet;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const/4 v0, 0x0

    .line 1398
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->ALWAYS:Ljava/lang/Integer;

    const/4 v0, 0x1

    .line 1399
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->NEVER:Ljava/lang/Integer;

    const/4 v0, 0x2

    .line 1400
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->AFTER_VIEWING_FAQS:Ljava/lang/Integer;

    const/4 v0, 0x3

    .line 1401
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->AFTER_MARKING_ANSWER_UNHELPFUL:Ljava/lang/Integer;

    .line 1403
    invoke-static {}, Lcom/helpshift/support/SupportInternal$EnableContactUs;->getSupportedValueSet()Ljava/util/HashSet;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/SupportInternal$EnableContactUs;->valueSet:Ljava/util/HashSet;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 1397
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static getSupportedValueSet()Ljava/util/HashSet;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashSet<",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation

    .line 1406
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 1407
    sget-object v1, Lcom/helpshift/support/SupportInternal$EnableContactUs;->ALWAYS:Ljava/lang/Integer;

    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 1408
    sget-object v1, Lcom/helpshift/support/SupportInternal$EnableContactUs;->NEVER:Ljava/lang/Integer;

    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 1409
    sget-object v1, Lcom/helpshift/support/SupportInternal$EnableContactUs;->AFTER_VIEWING_FAQS:Ljava/lang/Integer;

    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 1410
    sget-object v1, Lcom/helpshift/support/SupportInternal$EnableContactUs;->AFTER_MARKING_ANSWER_UNHELPFUL:Ljava/lang/Integer;

    invoke-virtual {v0, v1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    return-object v0
.end method
