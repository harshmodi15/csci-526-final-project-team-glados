using UnityEngine;
using System.Collections.Generic;

public class PortalManager : MonoBehaviour
{
    [Header("Portal Settings")]
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private int maxPortals = 2;
    [SerializeField] private LayerMask portalPlacementMask;
    [SerializeField] private float minPortalDistance = 1f;

    private List<Portal> activePortals = new List<Portal>();
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandlePortalCreation();
    }

    private void HandlePortalCreation()
    {
        // Left click for blue portal
        if (Input.GetMouseButtonDown(0))
        {
            CreatePortal(PortalType.Blue);
        }
        // Right click for orange portal
        else if (Input.GetMouseButtonDown(1))
        {
            CreatePortal(PortalType.Orange);
        }
    }

    private void CreatePortal(PortalType type)
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, portalPlacementMask);
        if (hit.collider != null)
        {
            // Check if we can place a portal here
            if (!IsValidPortalPosition(hit.point))
                return;
            // Remove existing portal of same type if it exists
            RemovePortalOfType(type);

            // Create new portal
            GameObject portalObj = Instantiate(portalPrefab, hit.point, Quaternion.identity);
            Portal portal = portalObj.GetComponent<Portal>();
            portal.Initialize(type);
            activePortals.Add(portal);

            // Link portals if we have a pair
            if (activePortals.Count == 2)
            {
                LinkPortals();
            }
        }
    }

    private bool IsValidPortalPosition(Vector2 position)
    {
        // Check distance from other portals
        foreach (Portal portal in activePortals)
        {
            if (Vector2.Distance(position, portal.transform.position) < minPortalDistance)
            {
                return false;
            }
        }
        return true;
    }

    private void RemovePortalOfType(PortalType type)
    {
        Portal portalToRemove = activePortals.Find(p => p.Type == type);
        if (portalToRemove != null)
        {
            activePortals.Remove(portalToRemove);
            Destroy(portalToRemove.gameObject);
        }
    }

    private void LinkPortals()
    {
        if (activePortals.Count != 2) return;

        Portal portal1 = activePortals[0];
        Portal portal2 = activePortals[1];

        portal1.LinkedPortal = portal2;
        portal2.LinkedPortal = portal1;
    }
}

public enum PortalType
{
    Blue,
    Orange
}