.class public Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;
.super Ljava/lang/Object;
.source "CustomContactUsFlowListHolder.java"


# static fields
.field private static flowList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getFlowList()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation

    .line 9
    sget-object v0, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->flowList:Ljava/util/List;

    return-object v0
.end method

.method public static setFlowList(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)V"
        }
    .end annotation

    .line 13
    sput-object p0, Lcom/helpshift/support/flows/CustomContactUsFlowListHolder;->flowList:Ljava/util/List;

    return-void
.end method
